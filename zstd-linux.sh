#!/bin/bash
docker run --rm -v "$PWD/builder/zstd:/builder" -v "$PWD/zstd:/src" --entrypoint /bin/sh alpine:latest /builder/zstd-builder.sh
ls zstd/lib/libzstd.a
ls zstd/lib/libzstd.so*
