# specify link lib
LIBDIR   := ../lib
# include implementation
LIBFILES  = $(wildcard $(LIBDIR)/*.c)
# concat all files
SRCFILES  = $(sort $(LIBFILES) $(wildcard *.c))
# wrapper.c -> wrapper.o
OBJFILES  = $(SRCFILES:.c=.o)
SHARED_NAMESPACE ?= LIBWRAPPER_
EXT ?=

CC = gcc # use `gcc`, instead of `cc`
CPPFLAGS += -I$(LIBDIR)
CFLAGS ?= -O3 -Wall -Wextra # automatically use this flag when build .c
FLAGS     = $(CFLAGS) $(CPPFLAGS) $(LDFLAGS)

.PHONY: program
program: wrapper

.PHONY: wrapper
wrapper: $(OBJFILES)
	$(CC) $(FLAGS) $(OBJFILES) -o $@$(EXT) -L . -lfiboedit

.PHONY: clean
clean:
	$(RM) wrapper
	$(RM) *.o
