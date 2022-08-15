:: run on cmd
docker run --rm -v "%cd%/builder/zstd:/builder" -v "%cd%/zstd:/src" alpine:latest /bin/sh /builder/zstd-builder.sh
dir zstd\lib\libzstd.a
dir zstd\lib\libzstd.so*
