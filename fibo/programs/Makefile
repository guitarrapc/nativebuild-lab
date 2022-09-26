# specify link lib
LIBDIR   := ../lib
# include implementation
LIBFILES  = $(wildcard $(LIBDIR)/*.c)
# concat all files
SRCFILES  = $(sort $(LIBFILES) $(wildcard *.c))
# fibo.c -> fibo.o
OBJFILES  = $(SRCFILES:.c=.o)
SHARED_NAMESPACE ?= LIBFIBO_
EXT ?=

CC = gcc # use `gcc`, instead of `cc`
CPPFLAGS += -I$(LIBDIR)
CFLAGS ?= -O3 -Wall -Wextra # automatically use this flag when build .c
FLAGS     = $(CFLAGS) $(CPPFLAGS) $(LDFLAGS)

.PHONY: program
program: fibo

.PHONY: fibo
fibo: $(OBJFILES)
	$(CC) $(FLAGS) $(OBJFILES) -o $@$(EXT) $(LDLIBS)

.PHONY: clean
clean:
	$(RM) fibo
	$(RM) *.o
