:: run on cmd
docker run --rm -v "%cd%/builder/zstd:/builder" -v "%cd%/zstd:/src" --entrypoint /bin/sh alpine:latest -c /builder/zstd-builder.sh
dir zstd\lib\libzstd.a
dir zstd\lib\libzstd.so*
