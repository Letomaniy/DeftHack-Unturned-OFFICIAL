using System;
 
[Flags]
public enum MemoryProtection : uint
{ 
    EXECUTE = 16u,
    EXECUTE_READ = 32u,
    EXECUTE_READWRITE = 64u,
    EXECUTE_WRITECOPY = 128u,
    NOACCESS = 1u,
    READONLY = 2u,
    READWRITE = 4u,
    WRITECOPY = 8u,
    GUARD_Modifierflag = 256u,
    NOCACHE_Modifierflag = 512u,
    WRITECOMBINE_Modifierflag = 1024u
}

