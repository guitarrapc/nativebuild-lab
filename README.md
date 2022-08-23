[![zstd build](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)

# nativebuild-lab

This repository indicate build native binaries for following.

* zstd
* (upcoming....)

<!-- START doctoc -->
<!-- END doctoc -->

# Basic policy

This repository will introduce following policy to build native binaries.

* Linux: Use package's standard build. If nothing prefered then use `make` when available.
* Windows: Use package's standard build. If nothing prefered then use `VC++` (w/CMake) when available. Alternative is `mingw-w64` on Docker.
* macOS: Use package's standard build. If nothing prefered then use `make` when available.
* iOS: Use CMake to build.
* Android: Use CMake to build.

# Build packages

Checkout repository, then run your build.

```bash
git clone https://github.com/guitarrapc/nativebuild-lab
git submodule update --init --recursive

# run your build for packages. see below instructions.
```

# zstd

Building [zstd](https://github.com/facebook/zstd) for following environment.

OS | Architecture | Build Env| Builder | Build Script | CI
---- | ---- | ---- | ---- | ---- | ----
Linux   | arm64 | Docker | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Linux   | x64   | Docker | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Windows | arm64 | Windows (alt. Docker) | VC++ (alt. mingw-w64) | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Windows | x64   | Windows (alt. Docker) | VC++ (alt. mingw-w64) | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Windows | x86   | Windows (alt. Docker) | VC++ (alt. mingw-w64) | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
macOS   | arm64 | Intel Mac, Apple Silicon Mac | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
macOS   | x64   | Intel Mac, Apple Silicon Mac | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
iOS     | arm64 | Intel Mac, Apple Silicon Mac | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Android | armeabi-v7a | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Android | arm64-v8a   | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Android | x86         | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Android | x86_64      | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)

## Linux

Use `make` to build.

### Linux (amd64)

```bash
# windows
builder\zstd\zstd-linux-x64.bat

# linux
bash ./builder/zstd/zstd-linux-x64.sh
```

### Linux (aarch64)

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

### Windows (arm64)

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

### macOS (x86_64)

```bash
bash ./builder/zstd/zstd-darwin-x64.sh
```

### macOS (arm64)

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
bash ./builder/zstd/zstd-android-arm.sh
```

### Android (arm64-v8a)

```bash
bash ./builder/zstd/zstd-android-arm64.sh
```

### Android (x86)

```bash
bash ./builder/zstd/zstd-android-x86.sh
```

### Android (x86_64)

```bash
bash ./builder/zstd/zstd-android-x64.sh
```

# REF

## CMake

**Basics**

* CMake Windows: [Visual Studio 17 2022 — CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/generator/Visual%20Studio%2017%202022.html)

**Native Platform**

* CMake Android: [CMake  \|  Android NDK  \|  Android Developers](https://developer.android.com/ndk/guides/cmake?hl=ja)
* CMake iOS: [cmake\-toolchains\(7\) — CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/manual/cmake-toolchains.7.html#cross-compiling-for-ios-tvos-or-watchos)

Other way to build.

* Android CMake build: [leleliu008/ndk\-pkg](https://github.com/leleliu008/ndk-pkg)
* iOS Cmake build: [leleliu008/xcpkg](https://github.com/leleliu008/xcpkg)

## mingw-w64

* Linux mingw-w64 amd64 build: [Downloads \- MinGW\-w64](https://www.mingw-w64.org/downloads/)
* Linux mingw-w64 arm64 build: [mstorsjo/llvm\-mingw](https://github.com/mstorsjo/llvm-mingw)
