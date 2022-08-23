[![zstd build](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)

# nativebuild-lab

This repository indicate build native binaries for following.

* zstd
* (upcoming....)

# Build packages

Checkout repository, then run your build.

```bash
git clone https://github.com/guitarrapc/nativebuild-lab
git submodule update --init --recursive

# run your build for packages. see below instructions.
```

# zstd

* [x] Linux (x64)
* [x] Linux (arm64)
* [x] Windows (x64)
* [x] Windows (x86)
* [x] Windows (arm64)
* [x] macOS (x64)
* [x] macOS (arm64)
* [x] iOS
* [x] Android

## Linux

Use `make` to build.

### Linux (x64)

```bash
# windows
builder\zstd\zstd-linux-x64.bat

# linux
bash ./builder/zstd/zstd-linux-x64.sh
```

### Linux (ARM64)

Use `make`.

```bash
# windows
builder\zstd\zstd-linux-arm64.bat

# linux
bash ./builder/zstd/zstd-linux-arm64.sh
```

## Windows

There are 2way to build.

1. make
2. cmake

If you are runnning on Linux, use make.
If you are runnning on Windows use cmake. you can use make with docker.

> **Note**: **cmake** binaries are named `zstd*`, on the otherhand **make** binaries are `libzstd*`.

### Windows (x64)

**make**

```bash
# windows
builder\zstd\zstd-windows-x64.bat

# linux
bash ./builder/zstd/zstd-windows-x64.sh
```

**cmake**

Install Visual Studio 2022 `C++ Desktop Experience` package for cmake.

Open `x64_x86 Cross Tools Command Prompt for VS2022`.

```bash
builder\zstd\zstd-windows-x64-cmake.bat
```

### Windows (x86)

**make**

```bash
# windows
builder\zstd\zstd-windows-x86.bat

# linux
bash ./builder/zstd/zstd-windows-x86.sh
```

**cmake**

Install Visual Studio 2022 `C++ Desktop Experience` package for cmake.

Open `x64_x86 Cross Tools Command Prompt for VS2022`.

```bash
builder\zstd\zstd-windows-x86-cmake.bat
```

### Windows (ARM64)

**make**

```bash
# windows
builder\zstd\zstd-windows-arm64.bat

# linux
bash ./builder/zstd/zstd-windows-arm64.sh
```

**cmake**

Install Visual Studio 2022 `C++ Desktop Experience` package and `MSVC v143 - VS 2022 C++ ARM64 build tools` for cmake.

Open `x64_x86 Cross Tools Command Prompt for VS2022`.

```bash
builder\zstd\zstd-windows-arm64-cmake.bat
```


## macOS

Use `make`.

### macOS (x64)

```bash
bash ./builder/zstd/zstd-darwin-x64.sh
```

### macOS (ARM64)

```bash
bash ./builder/zstd/zstd-darwin-arm64.sh
```

## iOS

use `cmake` or `xcpkg`.

**cmake**

Install prerequisites.
```bash
brew install cmake ninja gsed tree
```

```bash
bash ./builder/zstd/zstd-ios-arm64.sh
```

**xcpkg**

Install [xcpkg](https://github.com/leleliu008/xcpkg).

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

use `cmake`.

### Android (armeabi-v7a)

```bash
bash ./builder/zstd/zstd-android-arm64-v8a.sh
```

### Android (arm64-v8a)

```bash
bash ./builder/zstd/zstd-android-arm64-v8a.sh
```

### Android (x86)

```bash
bash ./builder/zstd/zstd-android-x86.sh
```

### Android (x64)

```bash
bash ./builder/zstd/zstd-android-x86_64.sh
```

## REF

http://blog.fpliu.com/it/software/zstd/build-for-current-host
http://blog.fpliu.com/project/ndk-pkg/package?name=zstd
http://blog.fpliu.com/project/xcpkg/package?name=zstd
