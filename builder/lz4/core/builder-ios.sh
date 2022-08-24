#!/bin/bash
set -eu

NATIVE_OS_KIND=$(uname | tr "[:upper:]" "[:lower:]") # should be darwin
BUILD_TYPE=Release # Release or Debug

IOS_VERSION=${IOS_VERSION:=8.0}
IOS_ARCH=${IOS_ARCH:=arm64}
TARGET="iPhoneOS/${IOS_ARCH}/${IOS_VERSION}"

INSTALL_DIR="$(pwd)/${SRC_DIR}/build/cmake/build/ios/${IOS_ARCH}"
MAKE_DIR="$(pwd)/${SRC_DIR}"
BUILD_DIR="${MAKE_DIR}"

PACKAGE_INCLUDES=""

COLOR_RED='\033[0;31m'          # Red
COLOR_GREEN='\033[0;32m'        # Green
COLOR_YELLOW='\033[0;33m'       # Yellow
COLOR_BLUE='\033[0;94m'         # Blue
COLOR_PURPLE='\033[0;35m'       # Purple
COLOR_OFF='\033[0m'             # Reset

step() {
    STEP_NUM=$((${STEP_NUM-0} + 1))
    STEP_MESSAGE="$*"
    printf '\n'
    printf '%b\n' "${COLOR_PURPLE}=>> STEP ${STEP_NUM} : ${STEP_MESSAGE} ${COLOR_OFF}"

    unset STEP2_NUM # reset
}
step2() {
    STEP2_NUM=$((${STEP2_NUM-0} + 1))
    STEP2_MESSAGE="$*"
    printf '\n'
    printf '%b\n' "${COLOR_BLUE}>>> STEP ${STEP_NUM}.${STEP2_NUM} : ${STEP2_MESSAGE} ${COLOR_OFF}"
}
die() {
  printf '%b\n' "${COLOR_RED}💔  $*${COLOR_OFF}" >&2
  exit 1
}

# __find_build_toolchains iPhoneOS/arm64/8.0
__find_build_toolchains() {
  step "Find build toolchains."

  if [ "$NATIVE_OS_KIND" != 'darwin' ] ; then
    die "this software can only be run on macOS."
  fi

  TARGET_OS_NAME=$(printf '%s\n' "${TARGET}" | cut -d/ -f1)
  TARGET_OS_VERS=$(printf '%s\n' "${TARGET}" | cut -d/ -f3)
  TARGET_OS_ARCH=$(printf '%s\n' "${TARGET}" | cut -d/ -f2)
  TARGET_OS_NAME_LOWER_CASE="$(echo "$TARGET_OS_NAME" | tr "[:upper:]" "[:lower:]")"

  PACKAGE_CDEFINE="__arm64__"

  # should be "/Applications/Xcode.app/Contents/Developer"
  TOOLCHAIN_ROOT="$(xcode-select -p)"

  [ -z "$TOOLCHAIN_ROOT" ] && die "please run command 'xcode-select --switch DIR', then try again."
  [ -d "$TOOLCHAIN_ROOT" ] || die "TOOLCHAIN_ROOT=$TOOLCHAIN_ROOT directory is not exist."

  TOOLCHAIN_BIND="$TOOLCHAIN_ROOT/Toolchains/XcodeDefault.xctoolchain/usr/bin"
  SYSROOT="$TOOLCHAIN_ROOT/Platforms/${TARGET_OS_NAME}.platform/Developer/SDKs/${TARGET_OS_NAME}.sdk"
  SYSTEM_LIBRARY_DIR="$SYSROOT/usr/lib"

  ZSTD_INSTALL_DIR="${INSTALL_DIR}/${IOS_ARCH}"

  CMAKE_TOOLCHAIN="$(pwd)/builder/${SRC_DIR}/ios-arm64.toolchain.cmake"

  AR="${TOOLCHAIN_BIND}/ar"
  CC="$TOOLCHAIN_BIND/clang"
  CXX="$TOOLCHAIN_BIND/clang++"

  CCFLAGS="-isysroot $SYSROOT -arch $TARGET_OS_ARCH -m${TARGET_OS_NAME_LOWER_CASE}-version-min=$TARGET_OS_VERS -Qunused-arguments -Os -pipe"
  CPPFLAGS="-isysroot $SYSROOT -Qunused-arguments"
  LDFLAGS="-isysroot $SYSROOT -arch $TARGET_OS_ARCH -m${TARGET_OS_NAME_LOWER_CASE}-version-min=$TARGET_OS_VERS"

  for item in $PACKAGE_INCLUDES; do
    CPPFLAGS="-I${item} $CPPFLAGS"
  done

  if [[ "$BUILD_TYPE" == "Release" ]] ; then
    CCFLAGS="$CCFLAGS -Wl,-S -Os -DNDEBUG"
    LDFLAGS="$LDFLAGS -Wl,-S"
  fi

  for item in $PACKAGE_CDEFINE; do
    CPPFLAGS="$CPPFLAGS -D$item"
  done

  CXXFLAGS="$CCFLAGS"
}

__print_build_toolchains() {
  step "Print build toolchains."

  cat <<EOF
        BUILD_TYPE = ${BUILD_TYPE}

    NATIVE_OS_KIND = ${NATIVE_OS_KIND}

    TARGET_OS_NAME = ${TARGET_OS_NAME}
    TARGET_OS_VERS = ${TARGET_OS_VERS}
    TARGET_OS_ARCH = ${TARGET_OS_ARCH}

           SYSROOT = ${SYSROOT}
SYSTEM_LIBRARY_DIR = ${SYSTEM_LIBRARY_DIR}
    TOOLCHAIN_BIND = ${TOOLCHAIN_BIND}

   CMAKE_TOOLCHAIN = ${CMAKE_TOOLCHAIN}
                CC = ${CC}
               CXX = ${CXX}
                AR = ${AR}

           CCFLAGS = ${CCFLAGS}
          CPPFLAGS = ${CPPFLAGS}
          CXXFLAGS = ${CXXFLAGS}
           LDFLAGS = ${LDFLAGS}

       INSTALL_DIR = ${INSTALL_DIR}
EOF

}

__install_lz4() {
  step "Install lz4."

  # create directory
  rm -rf "${INSTALL_DIR}"
  mkdir -p "${INSTALL_DIR}"

  # build
  pushd "${BUILD_DIR}" > /dev/null 2>&1
    make -w -j8 -C "${BUILD_DIR}" clean TARGET_OS=Darwin
    make -w -j8 -C "${BUILD_DIR}" install TARGET_OS=Darwin PREFIX="${INSTALL_DIR}" CC="${CC}" CFLAGS="${CCFLAGS}" LDFLAGS="${LDFLAGS}" AR="${AR}" BUILD_STATIC=yes BUILD_SHARED=yes

  popd > /dev/null 2>&1

  # cleanup
  unset BUILD_DIR
  unset PACKAGE_INCLUDES
}

__find_build_toolchains
__print_build_toolchains
__install_lz4
