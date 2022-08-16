#!/bin/bash

set -ex

ZSTD_VERSION=$(cd zstd && echo "$(git tag --points-at HEAD | tr -d '[:space:]')" && cd ..)
GIT_ZSTD_VERSION=${ZSTD_VERSION}
FILE_ZSTD_VERSION=$(echo "${ZSTD_VERSION}" | cut -c 2-)
OS=iPhoneOS
PLATFORM=arm64

NATIVE_OS_KIND=$(uname | tr A-Z a-z) # should be darwin
BUILD_TYPE=Release # Release or Debug
PACKAGE_NAME=zstd
TARGET=${OS}/${PLATFORM}/8.0

WORKING_DIR_BASE="$(pwd)/builder/zstd_ios/${FILE_ZSTD_VERSION}"
WORKING_DIR_CMAKE="$(pwd)/zstd/build/cmake"
WORKING_DIR_BUILD="${WORKING_DIR_BASE}/${OS}/${PLATFORM}"
INSTALL_DIR="$(pwd)/tmp/${OS}/${PLATFORM}"
TOOLCHAIN_FILE="$(pwd)/builder/zstd/toolchain.cmake"

# NOTE: zlib and lzma will found from iPhoneOS.sdk.
# -- Found ZLIB: /Applications/Xcode.app/Contents/Developer/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk/usr/lib/libz.tbd (found version "1.2.11")
# -- Found LibLZMA: /Applications/Xcode.app/Contents/Developer/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk/usr/lib/liblzma.tbd (found version "5.2.5")

die() {
    printf '%b\n' "${COLOR_RED}💔  $*${COLOR_OFF}" >&2
    exit 1
}

__clean() {
  rm -rf "${WORKING_DIR_BASE}"
  mkdir -p "${WORKING_DIR_BUILD}"
}

# __find_build_toolchains zstd iPhoneOS/arm64/8.0
__find_build_toolchains() {
    if [ "$NATIVE_OS_KIND" != 'darwin' ] ; then
        die "this software can only be run on macOS."
    fi

    TARGET_OS_NAME=$(printf '%s\n' "$2" | cut -d/ -f1)
    TARGET_OS_VERS=$(printf '%s\n' "$2" | cut -d/ -f3)
    TARGET_OS_ARCH=$(printf '%s\n' "$2" | cut -d/ -f2)
    TARGET_OS_NAME_LOWER_CASE="$(echo "$TARGET_OS_NAME" | tr "[:upper:]" "[:lower:]")"

    PACKAGE_CDEFINE="__arm64__"

    # should be "/Applications/Xcode.app/Contents/Developer"
    TOOLCHAIN_ROOT="$(xcode-select -p)"

    [ -z "$TOOLCHAIN_ROOT" ] && die "please run command 'xcode-select --switch DIR', then try again."
    [ -d "$TOOLCHAIN_ROOT" ] || die "TOOLCHAIN_ROOT=$TOOLCHAIN_ROOT directory is not exist."

    TOOLCHAIN_BIND="$TOOLCHAIN_ROOT/Toolchains/XcodeDefault.xctoolchain/usr/bin"
    SYSROOT="$TOOLCHAIN_ROOT/Platforms/${TARGET_OS_NAME}.platform/Developer/SDKs/${TARGET_OS_NAME}.sdk"
    SYSTEM_LIBRARY_DIR="$SYSROOT/usr/lib"

    CC="$TOOLCHAIN_BIND/clang"
    CXX="$TOOLCHAIN_BIND/clang++"

    CCFLAGS="-isysroot $SYSROOT -arch $TARGET_OS_ARCH -m${TARGET_OS_NAME_LOWER_CASE}-version-min=$TARGET_OS_VERS -Qunused-arguments -Os -pipe"
    CPPFLAGS="-isysroot $SYSROOT -Qunused-arguments"
    LDFLAGS="-isysroot $SYSROOT -arch $TARGET_OS_ARCH -m${TARGET_OS_NAME_LOWER_CASE}-version-min=$TARGET_OS_VERS"

    if [[ "$BUILD_TYPE" == "Release" ]] ; then
        CCFLAGS="$CCFLAGS -Wl,-S -Os -DNDEBUG"
        LDFLAGS="$LDFLAGS -Wl,-S"
    fi

    for item in $PACKAGE_CDEFINE; do
        CPPFLAGS="$CPPFLAGS -D$item"
    done

    CXXFLAGS="$CCFLAGS"
}

__config_cmake_variables() {
    CMAKE_VERBOSE_MAKEFILE=ON
    CMAKE_COLOR_MAKEFILE=ON

    BUILD_SHARED_LIBS=ON

    CMAKE_BUILD_TYPE=$BUILD_TYPE

    CMAKE_SYSTEM_NAME=Darwin
    CMAKE_SYSTEM_VERSION=8.0
    CMAKE_SYSTEM_PROCESSOR=arm64

    CMAKE_ASM_COMPILER="$CC"
    CMAKE_ASM_FLAGS="-arch arm64"

    CMAKE_C_COMPILER="$CC"
    CMAKE_C_FLAGS="$CCFLAGS $CPPFLAGS $LDFLAGS"

    CMAKE_CXX_COMPILER="$CXX"
    CMAKE_CXX_FLAGS="$CXXFLAGS $CPPFLAGS $LDFLAGS"

    CMAKE_AR="$TOOLCHAIN_BIND/ar"
    CMAKE_NM="$TOOLCHAIN_BIND/nm"
    CMAKE_RANLIB="$TOOLCHAIN_BIND/ranlib"
    CMAKE_STRIP="$TOOLCHAIN_BIND/strip"

    CMAKE_OSX_SYSROOT="$SYSROOT"

    # https://cmake.org/cmake/help/latest/variable/CMAKE_OSX_ARCHITECTURES.html
    CMAKE_OSX_ARCHITECTURES="${TARGET_OS_ARCH}\" CACHE STRING \""

    CMAKE_FIND_DEBUG_MODE=OFF

    CMAKE_LIBRARY_PATH="$SYSTEM_LIBRARY_DIR"
}

__create_cmake_toolchain_file() {
    cat <<EOF
set(CMAKE_VERBOSE_MAKEFILE $CMAKE_VERBOSE_MAKEFILE)
set(CMAKE_COLOR_MAKEFILE   $CMAKE_COLOR_MAKEFILE)

set(BUILD_SHARED_LIBS $BUILD_SHARED_LIBS)

set(CMAKE_BUILD_TYPE  $CMAKE_BUILD_TYPE)

set(CMAKE_SYSTEM_NAME      $CMAKE_SYSTEM_NAME)
set(CMAKE_SYSTEM_VERSION   $CMAKE_SYSTEM_VERSION)
set(CMAKE_SYSTEM_PROCESSOR $CMAKE_SYSTEM_PROCESSOR)

set(CMAKE_ASM_COMPILER $CMAKE_ASM_COMPILER)
set(CMAKE_ASM_FLAGS "$CMAKE_ASM_FLAGS")

set(CMAKE_C_COMPILER "$CMAKE_C_COMPILER")
set(CMAKE_C_FLAGS "$CMAKE_C_FLAGS")

set(CMAKE_CXX_COMPILER "$CMAKE_CXX_COMPILER")
set(CMAKE_CXX_FLAGS "$CMAKE_CXX_FLAGS")

set(CMAKE_AR     "$CMAKE_AR")
set(CMAKE_NM     "$CMAKE_NM")
set(CMAKE_RANLIB "$CMAKE_RANLIB")
set(CMAKE_STRIP  "$CMAKE_STRIP")

set(CMAKE_OSX_SYSROOT "$CMAKE_OSX_SYSROOT")

set(CMAKE_OSX_ARCHITECTURES "${CMAKE_OSX_ARCHITECTURES}")

set(CMAKE_FIND_DEBUG_MODE $CMAKE_FIND_DEBUG_MODE)

set(CMAKE_LIBRARY_PATH "$CMAKE_LIBRARY_PATH")
EOF
}

__clean
__find_build_toolchains "${PACKAGE_NAME}" "${TARGET}"
__config_cmake_variables
__create_cmake_toolchain_file | tee "${TOOLCHAIN_FILE}"

#/opt/homebrew/bin/cmake -Wno-dev -S /var/folders/hj/ht6w56yd0xj4l19j282jnf4c0000gn/T/tmp.JLcL2soG/build/cmake -B /var/folders/hj/ht6w56yd0xj4l19j282jnf4c0000gn/T/tmp.JLcL2soG/1660633136/iPhoneOS/arm64 -DCMAKE_INSTALL_PREFIX=/Users/guitarrapc/.xcpkg/install.d/zstd/iPhoneOS/arm64 -DCMAKE_TOOLCHAIN_FILE=/var/folders/hj/ht6w56yd0xj4l19j282jnf4c0000gn/T/tmp.JLcL2soG/1660633136/iPhoneOS/arm64/toolchain.cmake -DCMAKE_VERBOSE_MAKEFILE=ON -DCMAKE_COLOR_MAKEFILE=ON -DZSTD_MULTITHREAD_SUPPORT=ON -DZSTD_BUILD_TESTS=OFF -DZSTD_BUILD_CONTRIB=OFF -DZSTD_BUILD_PROGRAMS=ON -DZSTD_BUILD_STATIC=ON -DZSTD_BUILD_SHARED=ON -DZSTD_ZLIB_SUPPORT=ON -DZSTD_LZMA_SUPPORT=ON -DZSTD_LZ4_SUPPORT=OFF -DZLIB_INCLUDE_DIR=/Users/guitarrapc/.xcpkg/install.d/zlib/iPhoneOS/arm64/include -DZLIB_LIBRARY=/Users/guitarrapc/.xcpkg/install.d/zlib/iPhoneOS/arm64/lib/libz.a -DLIBLZMA_INCLUDE_DIR=/Users/guitarrapc/.xcpkg/install.d/xz/iPhoneOS/arm64/include -DLIBLZMA_LIBRARY=/Users/guitarrapc/.xcpkg/install.d/xz/iPhoneOS/arm64/lib/liblzma.a
#/opt/homebrew/bin/cmake --build /var/folders/hj/ht6w56yd0xj4l19j282jnf4c0000gn/T/tmp.JLcL2soG/1660633136/iPhoneOS/arm64 -- -j8

pushd ${WORKING_DIR_BASE}
cmake -Wno-dev -S "${WORKING_DIR_CMAKE}" -B "${WORKING_DIR_BUILD}" -DCMAKE_INSTALL_PREFIX="${INSTALL_DIR}" -DCMAKE_TOOLCHAIN_FILE="${TOOLCHAIN_FILE}" -DCMAKE_VERBOSE_MAKEFILE=ON -DCMAKE_COLOR_MAKEFILE=ON -DZSTD_MULTITHREAD_SUPPORT=ON -DZSTD_BUILD_TESTS=OFF -DZSTD_BUILD_CONTRIB=OFF -DZSTD_BUILD_PROGRAMS=ON -DZSTD_BUILD_STATIC=ON -DZSTD_BUILD_SHARED=ON -DZSTD_LZ4_SUPPORT=OFF -DZSTD_ZLIB_SUPPORT=ON -DZSTD_LZMA_SUPPORT=ON
cmake --build "${WORKING_DIR_BUILD}"  -- -j8
popd

# confirm
ls -l "${WORKING_DIR_BUILD}/lib/libzstd.a"
ls -l ${WORKING_DIR_BUILD}/lib/libzstd.*dylib

# copy
mkdir -p "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/"
cp "${WORKING_DIR_BUILD}/lib/libzstd.a" "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/."
cp "${WORKING_DIR_BUILD}/lib/libzstd.${FILE_ZSTD_VERSION}.dylib" "./pkg/zstd/${GIT_ZSTD_VERSION}/${OS}/${PLATFORM}/libzstd.dylib"
