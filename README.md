nativebuild-lab

# zstd

* Linux: /pkg/usr/local/lib (Docker)
* Windows: zstd/build.d/lib/Release (Repo)
* macOS: [TBD]
* iOS: [TBD]
* Android: [TBD]

## Linux

**Docker**

You can build with Docker.

```bash
docker build -t zstd:latest -f builder/zstd-linux.dockerfile zstd
docker run -i --rm --entrypoint /bin/sh zstd -c "ls -l /pkg/usr/local/lib"

-rw-r--r--    1 root     root       1195726 Aug 10 06:59 libzstd.a
lrwxrwxrwx    1 root     root            16 Aug 10 06:59 libzstd.so -> libzstd.so.1.5.3
lrwxrwxrwx    1 root     root            16 Aug 10 06:59 libzstd.so.1 -> libzstd.so.1.5.3
-rwxr-xr-x    1 root     root       1016320 Aug 10 06:59 libzstd.so.1.5.3
drwxr-xr-x    2 root     root          4096 Aug 10 06:59 pkgconfig
```

Following will work.

```bash
cd zstd
docker build -t zstd:latest -f contrib/docker/Dockerfile .
echo foo | docker run -i --rm zstd | docker run -i --rm zstd zstdcat
```

## Windows

**cmake**

Use cmake.exe to build zstd.

Install Visual Studio 2022 `C++ Desktop Experience` package for cmake.

```bash
cd zstd
cmake -DCMAKE_BUILD_TYPE=Release -DCMAKE_INSTALL_PREFIX=../usr -G "Visual Studio 17 2022" -Wno-dev -S build/cmake -B build.d
cd build.d
cmake --build ./build.d --config Release
```

```bash
ls -l build.d/lib/Release
total 2032
-rw-rw-r--    1 S07671   S07671      599552 Aug 10 18:19 zstd.dll
-rw-rw-r--    1 S07671   S07671       26362 Aug 10 18:19 zstd.exp
-rw-rw-r--    1 S07671   S07671       43314 Aug 10 18:19 zstd.lib
-rw-rw-r--    1 S07671   S07671     1404092 Aug 10 18:19 zstd_static.lib
```

## macOS

## iOS

## Android
