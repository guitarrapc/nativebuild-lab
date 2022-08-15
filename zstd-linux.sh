#!/bin/bash
docker run -it --rm -v "$PWD/builder/zstd:/builder" -v "$PWD/zstd:/src" --entrypoint /bin/sh alpine:latest -c /builder/zstd-builder.sh
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.so*
