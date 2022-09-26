# references: see https://docs.oracle.com/cd/E19504-01/802-5880/6i9k05dhg/index.html
# COMPILE.c: make's predefined Macro. same as `COMPILE.c=$(CC) $(CFLAGS) $(CPPFLAGS) -c`
# wildcard *.c: get all *.c files in directory

LIBFIBOA ?= libfibo.a
LIBFIBOSO ?= libfibo.so
SHARED_NAMESPACE ?= LIBFIBO_
SRCFILES := $(sort $(wildcard *.c))

CC = gcc # use `gcc`, instead of `cc`
CPPFLAGS +=
CFLAGS ?= -O3 -Werror -Wall -Wextra # automatically use this flag when build .c
FLAGS    = $(CFLAGS) $(CPPFLAGS) $(LDFLAGS)

.PHONY: lib
lib: liba libso

.PHONY: liba
liba: $(LIBFIBOA)

.PHONY: $(LIBFIBOA)
$(LIBFIBOA): $(SRCFILES)
	@echo compiling static library
	$(COMPILE.c) $^
	$(AR) rcs $@ *.o

.PHONY: libso
libso: $(LIBFIBOSO)

.PHONY: $(LIBFIBOSO)
$(LIBFIBOSO): $(SRCFILES)
	@echo compiling dynamic library
	$(CC) $(FLAGS) -shared $^ -fPIC -fvisibility=hidden -o $@

.PHONY: debug
debug:
	@echo WINBASED $(WINDRES), TARGET_OS $(TARGET_OS) POSIX_ENV $(POSIX_ENV)

.PHONY: clean
clean:
	$(RM) *.a
	$(RM) *.o
	$(RM) *.so
