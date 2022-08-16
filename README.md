nativebuild-lab

```
git clone https://github.com/guitarrapc/nativebuild-lab
git submodule update --init --recursive
```


# zstd

* Linux amd: zstd/lib (make)
* Linux arm: zstd/lib (make aarch64build)
* Windows: zstd/build.d/lib/Release (cmake)
* macOS: zstd/lib (make)
* iOS: ~/.xcpkg/install.d/zstd/iPhoneOS/arm64/lib
* Android: [TBD]

## Linux amd

Use `make`.

```bash
# windows
zstd-linux-amd64.bat

# linux
. ./zstd-linux-amd64.sh
```

## Linux arm

Use `make`.

```bash
# windows
zstd-linux-aarch64.bat

# linux
. ./zstd-linux-aarch64.sh
```

## Windows

There are 2way to build.

1. make
2. cmake

Primary target is make.

**make**

```bash
# windows
zstd-windows-x64.bat

# linux
. ./zstd-windows-x64.sh
```

**cmake**

Install Visual Studio 2022 `C++ Desktop Experience` package for cmake.

```bash
cd zstd
cmake -DCMAKE_BUILD_TYPE=Release -DCMAKE_INSTALL_PREFIX=../usr -G "Visual Studio 17 2022" -Wno-dev -S build/cmake -B build.d
cmake --build ./build.d --config Release
```

```bash
ls -l build.d/lib/Release
total 2032
-rw-rw-r--    1 user   user      599552 Aug 10 18:19 zstd.dll
-rw-rw-r--    1 user   user       26362 Aug 10 18:19 zstd.exp
-rw-rw-r--    1 user   user       43314 Aug 10 18:19 zstd.lib
-rw-rw-r--    1 user   user     1404092 Aug 10 18:19 zstd_static.lib
```


## macOS

Use `make`.

```bash
. ./zstd-darwin.sh
```

If running mac is M1/M2, output binary will be `arm64`.
If running mac is Intel or Rosetta , output binary will be `x86_64` (=amd64).

## iOS

use `cmake` or `xcpkg`.

**cmake**

Install prerequisites.
```bash
brew install cmake ninja gsed tree
```

```bash
cd builder/zstd_ios
cmake ../../zstd/build/cmake -GXcode -DCMAKE_SYSTEM_NAME=iOS
cmake ../../zstd/build/cmake -B_builds -GXcode -DCMAKE_SYSTEM_NAME=iOS -DCMAKE_INSTALL_PREFIX=`pwd`/_install -DCMAKE_IOS_INSTALL_COMBINED=YES
cmake -S ../../zstd/build/cmake -B_builds -GXcode -DCMAKE_SYSTEM_NAME=iOS "-DCMAKE_OSX_ARCHITECTURES=armv7;armv7s;arm64;i386;x86_64" -DCMAKE_OSX_DEPLOYMENT_TARGET=9.3 -DCMAKE_INSTALL_PREFIX=`pwd`/_install -DCMAKE_XCODE_ATTRIBUTE_ONLY_ACTIVE_ARCH=NO -DCMAKE_IOS_INSTALL_COMBINED=YES
cmake --build _builds --config Release --target install
cd ../..
```

**xcpkg**

```bash
brew tap leleliu008/fpliu
brew install xcpkg
```

```bash
xcpkg update
sed -i '' -e "s|https://zlib.net/zlib-1.2.11.tar.gz|https://zlib.net/fossils/zlib-1.2.11.tar.gz|" "$HOME/.xcpkg/repos.d/offical/formula/zlib.sh"
xcpkg install zstd
```

```bash
% ls -l ~/.xcpkg/install.d/zstd/iPhoneOS/arm64/lib

total 2968
drwxr-xr-x  3 user  staff      96 Aug 10 19:46 cmake/
-rwxr-xr-x  1 user  staff  728072 Aug 10 19:46 libzstd.1.5.2.dylib*
lrwxr-xr-x  1 user  staff      19 Aug 10 19:46 libzstd.1.dylib@ -> libzstd.1.5.2.dylib
-rw-r--r--  1 user  staff  789512 Aug 10 19:46 libzstd.a
lrwxr-xr-x  1 user  staff      15 Aug 10 19:46 libzstd.dylib@ -> libzstd.1.dylib
drwxr-xr-x  3 user  staff      96 Aug 10 19:46 pkgconfig/
```

## Android


## REF

http://blog.fpliu.com/it/software/zstd/build-for-current-host
http://blog.fpliu.com/project/ndk-pkg/package?name=zstd
http://blog.fpliu.com/project/xcpkg/package?name=zstd
