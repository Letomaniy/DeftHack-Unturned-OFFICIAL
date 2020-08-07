using System;


 
[Flags]
public enum AllocationType : uint
{ 
    COMMIT = 4096u, 
    RESERVE = 8192u, 
    RESET = 524288u, 
    LARGE_PAGES = 536870912u, 
    PHYSICAL = 4194304u, 
    TOP_DOWN = 1048576u, 
    WRITE_WATCH = 2097152u
}

