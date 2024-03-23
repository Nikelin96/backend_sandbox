-module(hello).
-export([say_hi/0]).

say_hi() -> io:fwrite("Hi Mom\n").