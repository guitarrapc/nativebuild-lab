#!/bin/bash
apt-get update
apt-get install -yq --no-install-suggests --no-install-recommends build-essential pkg-config curl libssl-dev zlib1g-dev
apt-get install -yq --no-install-suggests --no-install-recommends unzip cmake ccache

INSTALL_ROOT=/root/install
INSTALLING_ROOT=/root/installing
SOURCE_ROOT=/root/source

ABI=armeabi-v7a
SYSTEM_PROCESSOR=armv7-a
ANDROID_PLATFORM=21

ANDROID_NDK=android-ndk-r23c
ANDROID_URL=https://dl.google.com/android/repository/${ANDROID_NDK}-linux.zip

ZLIB_URL=https://zlib.net/zlib-1.2.12.tar.gz
XZ_URL=https://downloads.sourceforge.net/project/lzmautils/xz-5.2.5.tar.gz

# cleanup
rm -rf /root/
mkdir -p ${INSTALL_ROOT}
mkdir -p ${INSTALLING_ROOT}
mkdir -p ${SOURCE_ROOT}

# install ndk
curl --retry 20 --retry-delay 30 -Lk -o "/root/${ANDROID_NDK}.zip" "${ANDROID_URL}"
unzip -q /root/${ANDROID_NDK}.zip -d /root/

################# zlib

# create directory
mkdir -p ${INSTALLING_ROOT}/zlib/src
mkdir -p ${INSTALLING_ROOT}/zlib/${ABI}/build
mkdir -p ${INSTALL_ROOT}/zlib

# download
curl --retry 20 --retry-delay 30 -Lk -o "${SOURCE_ROOT}/zlib.tar.gz" "${ZLIB_URL}"
tar xf ${SOURCE_ROOT}/zlib.tar.gz -C ${INSTALLING_ROOT}/zlib/src --strip-components 1

# cmake
cd ${INSTALLING_ROOT}/zlib/src
/usr/bin/cmake \
  -DCMAKE_TOOLCHAIN_FILE='/root/android-ndk-r23c/build/cmake/android.toolchain.cmake' \
  -DANDROID_ABI="${ABI}" \
  -DANDROID_PLATFORM=${ANDROID_PLATFORM} \
  -Wno-dev \
  -DCMAKE_EXPORT_COMPILE_COMMANDS=OFF \
  -DBUILD_TESTING=OFF \
  -DCMAKE_BUILD_TYPE=Release \
  -DCMAKE_VERBOSE_MAKEFILE=ON \
  -DCMAKE_COLOR_MAKEFILE=ON \
  -DCMAKE_SYSTEM_PROCESSOR="${SYSTEM_PROCESSOR}" \
  -DCMAKE_FIND_ROOT_PATH='' \
  -DCMAKE_LIBRARY_PATH='/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/arm-linux-androideabi/21' -DCMAKE_IGNORE_PATH='' \
  -DCMAKE_INSTALL_PREFIX='/root/install/zlib/armeabi-v7a' \
  -DCMAKE_C_FLAGS='--sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -fPIC -Wl,--as-needed -Wl,--strip-debug -Os -DNDEBUG -I/root/installing/zlib/armeabi-v7a/include -I/root/installing/zlib --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -I/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/include/arm-linux-androideabi --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -L/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/arm-linux-androideabi/21 -Wl,--as-needed -Wl,--strip-debug' \
  -DCMAKE_CXX_FLAGS='--sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -fPIC -Wl,--as-needed -Wl,--strip-debug -Os --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -I/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/include/arm-linux-androideabi --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -L/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/arm-linux-androideabi/21 -Wl,--as-needed -Wl,--strip-debug' \
  -DBUILD_SHARED_LIBS=ON \
  -DANDROID_TOOLCHAIN=clang \
  -DANDROID_ARM_NEON=TRUE \
  -DANDROID_STL=c++_shared \
  -DANDROID_USE_LEGACY_TOOLCHAIN_FILE=OFF \
  -DNDK_CCACHE=/usr/bin/ccache \
  -S /root/installing/zlib/src \
  -B /root/installing/zlib/armeabi-v7a/build

/usr/bin/cmake --build /root/installing/zlib/armeabi-v7a/build -- -j8
/usr/bin/cmake --install /root/installing/zlib/armeabi-v7a/build

# adjust ELF files for zlib
/root/${ANDROID_NDK}/toolchains/llvm/prebuilt/linux-x86_64/bin/llvm-strip /root/install/zlib/armeabi-v7a/lib/libz.so

##################### xz

# create directory
mkdir -p ${INSTALLING_ROOT}/xz/src
mkdir -p ${INSTALLING_ROOT}/xz/${ABI}/build
mkdir -p ${INSTALL_ROOT}/xz

# download
curl --retry 20 --retry-delay 30 -Lk -o "${SOURCE_ROOT}/xz.tar.gz" "${XZ_URL}"
tar xf ${SOURCE_ROOT}/xz.tar.gz -C ${INSTALLING_ROOT}/xz/src --strip-components 1

# cmake
cd ${INSTALLING_ROOT}/xz/${ABI}/build
/root/installing/xz/src/configure \
  --host='armv7a-linux-androideabi' \
  --prefix='/root/install/android/21/xz/armeabi-v7a' \
  --disable-option-checking \
  --disable-rpath \
  --disable-nls \
  --enable-largefile \
  CC='/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/bin/armv7a-linux-androideabi21-clang' \
  CFLAGS='--sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -fPIC -Wl,--as-needed -Wl,--strip-debug -Os -DNDEBUG' \
  CXX='/root/installingandroid-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/bin/armv7a-linux-androideabi21-clang++' \
  CXXFLAGS='--sysroot /root/installingandroid-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -fPIC -Wl,--as-needed -Wl,--strip-debug -Os -DNDEBUG' \
  CPP='/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/bin/armv7a-linux-androideabi21-clang -E' \
  CPPFLAGS='-I/root/installing/android/21/xz/armeabi-v7a/include -I/root/installing/android/21/xz --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -I/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/include/arm-linux-androideabi' \
  LDFLAGS='-L/root/installing/android/21/xz/armeabi-v7a/lib -L/root/installing/android/21/xz --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -L/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/arm-linux-androideabi/21 -Wl,--as-needed -Wl,--strip-debug' \
  AR='/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/bin/llvm-ar' \
  RANLIB='/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/bin/llvm-ranlib' \
  PKG_CONFIG='/usr/bin/pkg-config' \
  PKG_CONFIG_PATH='' \
  PKG_CONFIG_LIBDIR='' \
  CC_FOR_BUILD='/usr/bin/cc' \
  --disable-debug \
  --enable-static \
  --enable-shared \
  --disable-external-sha256 \
  --disable-werror \
  --enable-assembler \
  --enable-threads=posix

/usr/bin/gmake -w -C /root/installing/xz/armeabi-v7a/build -j8 clean
/usr/bin/gmake -w -C /root/installing/xz/armeabi-v7a/build -j8
/usr/bin/gmake -w -C /root/installing/xz/armeabi-v7a/build -j8 install

##################### zstd

# create directory
mkdir -p ${INSTALL_ROOT}/zstd
rm -rf /S /Q /src/build/cmake/build
mkdir /src/build/cmake/build

# build
cd /src/build/cmake/build
/usr/bin/cmake \
  -DCMAKE_TOOLCHAIN_FILE='/root/android-ndk-r23c/build/cmake/android.toolchain.cmake' \
  -DANDROID_ABI="${ABI}" \
  -DANDROID_PLATFORM=${ANDROID_PLATFORM} \
  -Wno-dev \
  -DCMAKE_EXPORT_COMPILE_COMMANDS=OFF \
  -DBUILD_TESTING=OFF \
  -DCMAKE_BUILD_TYPE=Release \
  -DCMAKE_VERBOSE_MAKEFILE=ON \
  -DCMAKE_COLOR_MAKEFILE=ON \
  -DCMAKE_SYSTEM_PROCESSOR=armv7-a \
  -DCMAKE_FIND_ROOT_PATH='/root/install/android/21/zlib/armeabi-v7a;/root/install/android/21/xz/armeabi-v7a' \
  -DCMAKE_LIBRARY_PATH='/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/arm-linux-androideabi/21' \
  -DCMAKE_IGNORE_PATH='/root/install/android/21/xz/armeabi-v7a/bin' \
  -DCMAKE_INSTALL_PREFIX='/root/install/android/21/zstd/armeabi-v7a' \
  -DCMAKE_C_FLAGS='--sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -fPIC -Wl,--as-needed -Wl,--strip-debug -Os -DNDEBUG -I/root/install/android/21/xz/armeabi-v7a/include -I/root/install/android/21/zlib/armeabi-v7a/include --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -I/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/include/arm-linux-androideabi --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -L/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/arm-linux-androideabi/21 -Wl,--as-needed -Wl,--strip-debug' \
  -DCMAKE_CXX_FLAGS='--sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -fPIC -Wl,--as-needed -Wl,--strip-debug -Os -DNDEBUG -I/root/install/android/21/xz/armeabi-v7a/include -I/root/install/android/21/zlib/armeabi-v7a/include --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -Qunused-arguments -I/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/include/arm-linux-androideabi --sysroot /root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot -L/root/android-ndk-r23c/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/arm-linux-androideabi/21 -Wl,--as-needed -Wl,--strip-debug' \
  -DBUILD_SHARED_LIBS=ON \
  -DANDROID_TOOLCHAIN=clang \
  -DANDROID_ARM_NEON=TRUE \
  -DANDROID_STL=c++_shared \
  -DANDROID_PLATFORM=21 \
  -DANDROID_USE_LEGACY_TOOLCHAIN_FILE=OFF \
  -DNDK_CCACHE=/usr/bin/ccache \
  -S /src/build/cmake \
  -B /src/build/cmake/build \
  -DZSTD_MULTITHREAD_SUPPORT=ON \
  -DZSTD_BUILD_TESTS=OFF \
  -DZSTD_BUILD_CONTRIB=OFF \
  -DZSTD_BUILD_PROGRAMS=ON \
  -DZSTD_BUILD_STATIC=ON \
  -DZSTD_BUILD_SHARED=ON \
  -DZSTD_ZLIB_SUPPORT=ON \
  -DZSTD_LZMA_SUPPORT=ON \
  -DZSTD_LZ4_SUPPORT=OFF

/usr/bin/cmake --build /src/build/cmake/build -- -j8
/usr/bin/cmake --install /src/build/cmake/build
