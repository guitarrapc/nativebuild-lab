#!/bin/sh
apk --no-cache add make gcc libc-dev
apk --no-cache add python3 perl
cd /src
make clean
make SHARED=1 lib
