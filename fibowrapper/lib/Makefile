# references: see https://docs.oracle.com/cd/E19504-01/802-5880/6i9k05dhg/index.html
# COMPILE.c: make's predefined Macro. same as `COMPILE.c=$(CC) $(CFLAGS) $(CPPFLAGS) -c`
# wildcard *.c: get all *.c files in directory

LIBWRAPPERA ?= libwrapper.a
LIBWRAPPERSO ?= libwrapper.so
SHARED_NAMESPACE ?= LIBWRAPPER_
SRCFILES := $(sort $(wildcard *.c))

CC ?= gcc # use `gcc`, instead of `cc`
CPPFLAGS += -DXXH_NAMESPACE=$(SHARED_NAMESPACE)
CFLAGS ?= -O3 -Werror -Wall -Wextra # automatically use this flag when build .c
FLAGS    = $(CFLAGS) $(CPPFLAGS) $(LDFLAGS)

.PHONY: lib
lib: liba libso

.PHONY: liba
liba: $(LIBWRAPPERA)

.PHONY: $(LIBWRAPPERA)
$(LIBWRAPPERA): $(SRCFILES)
	@echo compiling static library
	$(COMPILE.c) $^ -L . -lfibo
	$(AR) rcs $@ *.o

.PHONY: libso
libso: $(LIBWRAPPERSO)

.PHONY: $(LIBWRAPPERSO)
$(LIBWRAPPERSO): $(SRCFILES)
	@echo compiling dynamic library
	$(CC) $(FLAGS) -shared $^ -fPIC -fvisibility=hidden -o $@ -L . -lfibo

.PHONY: debug
debug:
	@echo WINBASED $(WINDRES), TARGET_OS $(TARGET_OS) POSIX_ENV $(POSIX_ENV)

.PHONY: clean
clean:
	$(RM) libwrapper.a
	$(RM) *.o
	$(RM) *.so
