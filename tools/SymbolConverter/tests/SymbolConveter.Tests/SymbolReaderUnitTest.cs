namespace SymbolConveter.Tests;

public class SymbolReaderUnitTest
{
    const string PREFIX = "THISIS_PREFIX_";

    private static readonly string[] Contents = @"
// extern
// extern const int comment_out_field;
externextern const foo_info_t do_not_read;
externextern void* (do_not_read_function)( );

extern const foo_info_t foo_info;
    extern int bar_int;
extern void (*function_pointer_foo)( const char * test, int line, const char * file );
    extern int ( *function_pointer_bar )();
extern void* (function_pointer_piyo)( );
extern FStar_UInt128_uint128
(*FStar_UInt128_op_Greater_Greater_Hat)(FStar_UInt128_uint128 x0, uint32_t x1);


// typedef
typedef struct singleline_typedef_struct_s singleline_typedef_struct_t;

// typedef uint64_t should_be_ignopre_typdef;
typedef uint64_t should_be_ignore_missing_semicolon
// typedef enum {
    VAL_1 = 0,
    VAL_2,
    VAL_3,
    VAL_4
} should_be_ignopre_typdef;

typedef uint64_t simple_t;
    typedef uint64_t simple_space_t;

typedef enum {
    VAL_1 = 0,
    VAL_2,
    VAL_3,
    VAL_4
} enum_t;

typedef struct inline_commennt_struct
{
    int struct_method(nr);                     /*!< The number of rounds. */
    uint32_t *struct_method(rk);               /*!< AES round keys. */
    uint32_t struct_method(buf)[68];           /*!< Unaligned data buffer. This buffer can
                                        hold 32 extra Bytes, which can be used for
                                        one of the following purposes:
                                        <ul><li>Alignment if VIA padlock is
                                                used.</li>
                                        <li>Simplifying key expansion in the 256-bit
                                            case by generating an extra round key.
                                            </li></ul> */
}
multiline_next_t;

typedef struct include_methods_comment
{
    mbedtls_aes_context typedef_struct_method(crypt); /*!< The AES context to use for AES block
                                        encryption or decryption. */
    mbedtls_aes_context typedef_struct_method(tweak); /*!< The AES context used for tweak
                                        computation. */
} multiline_t;

typedef struct
{
    struct key_data
    {
        uint8_t *data;
        size_t bytes;
    } inside;
} nested_t;

    typedef struct
    {
        void *key;
        mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
        mbedtls_pk_rsa_alt_sign_func sign_func;
        mbedtls_pk_rsa_alt_key_len_func key_len_func;
    } spaces_multiline_t;

typedef struct
{
    foo_t struct_method(alg);
    union
    {
        unsigned struct_method(dummy); /* Make the union non-empty even with no supported algorithms. */
#if defined(PIYO) || defined(PIYO_TEST)
        foo_t struct_method(hmac);
#endif /* PIYO */
#if defined(PIYO_FOO) || defined(PIYO_TEST)
        bar_t struct_method(cmac);
#endif /* PIYO_FOO */
    } struct_method(ctx);
} include_method_t;

// method
void foo();
    int foo_spaced( foo_context *ssl );

FIBOLIB_API void get_sample_data(sample_data_t *output);
    FIBOLIB_API int fibo(int n);

foo_bar_t foo_bar_method(
        const foo_piyo *transform );
    foo_uint foo_spacing_method( abc *d, size_t d_len ,
                                        const efg *s, size_t s_len,
                                        hij b );
                                            
int same_function_name( size_t foo,
                        const unsigned char *data );
int same_function_name( bar_context_t *ctx,
                        const unsigned char *add_data );
    
// ignores
struct foo_struct
{
    unsigned char *p;       /*!< message, including handshake headers   */
    size_t len;             /*!< length of p                            */
    unsigned char type;     /*!< type of the message: handshake or CCS  */
    mbedtls_ssl_flight_item *next;  /*!< next handshake message(s)              */
};

static inline int static_inline_method( const foo_context *ssl,
    const unsigned char **psk, size_t *psk_len )
{
    if( ssl->handshake->psk != NULL && ssl->handshake->psk_len > 0 )
    {
        *psk = ssl->handshake->psk;
        *psk_len = ssl->handshake->psk_len;
    }

    else if( ssl->conf->psk != NULL && ssl->conf->psk_len > 0 )
    {
        *psk = ssl->conf->psk;
        *psk_len = ssl->conf->psk_len;
    }

    else
    {
        *psk = NULL;
        *psk_len = 0;
        return( MBEDTLS_ERR_SSL_PRIVATE_KEY_REQUIRED );
    }

    return( 0 );
}

// void should_be_ignopre_method();
// FIBOLIB_API void should_be_ignopre_method(sample_data_t *output);
// foo_mode_t should_be_ignopre_method(
        const foo_transform_transform *transform );

/*
    * GCM multiplication: c = a times b in GF(2^128)
    * Based on [CLMUL-WP] algorithms 1 (with equation 27) and 5.
    */

#ifndef FIBOLIB_VISIBILITY
#  if defined(__GNUC__) && (__GNUC__ >= 4)
#    define FIBOLIB_VISIBILITY __attribute__ ((visibility (""default"")))
#  else
#    define FIBOLIB_VISIBILITY
#  endif
#endif

#define FOO_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h ) \
    FOO_BYTES_TO_T_UINT_4( a, b, c, d ),                \
    FOO_BYTES_TO_T_UINT_4( e, f, g, h )

#define FOO_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h )   \
    ( (foo_uint) (a) <<  0 ) |                        \
    ( (foo_uint) (b) <<  8 ) |                        \
    ( (foo_uint) (c) << 16 ) |                        \
    ( (foo_uint) (d) << 24 ) |                        \
    ( (foo_uint) (e) << 32 ) |                        \
    ( (foo_uint) (f) << 40 ) |                        \
    ( (foo_uint) (g) << 48 ) |                        \
    ( (foo_uint) (h) << 56 )

#define MULADDC_X8_CORE                         \
    ""movd     %%ecx,     %%mm1      \n\t""   \
    ""movd     %%ebx,     %%mm0      \n\t""   \
    ""movd     (%%edi),   %%mm3      \n\t""   \
    ""paddq    %%mm3,     %%mm1      \n\t""   \
    ""movd     (%%esi),   %%mm2      \n\t""   \
    ""pmuludq  %%mm0,     %%mm2      \n\t""   \
    ""movd     4(%%esi),  %%mm4      \n\t""   \
    ""pmuludq  %%mm0,     %%mm4      \n\t""   \
    ""movd     8(%%esi),  %%mm6      \n\t""   \
    ""pmuludq  %%mm0,     %%mm6      \n\t""   \
    ""movd     12(%%esi), %%mm7      \n\t""   \
    ""pmuludq  %%mm0,     %%mm7      \n\t""   \
    ""paddq    %%mm2,     %%mm1      \n\t""   \
    ""movd     4(%%edi),  %%mm3      \n\t""   \
    ""paddq    %%mm4,     %%mm3      \n\t""   \
    ""movd     8(%%edi),  %%mm5      \n\t""   \
    ""paddq    %%mm6,     %%mm5      \n\t""   \
    ""movd     12(%%edi), %%mm4      \n\t""   \
    ""paddq    %%mm4,     %%mm7      \n\t""   \
    ""movd     %%mm1,     (%%edi)    \n\t""   \
    ""movd     16(%%esi), %%mm2      \n\t""   \
    ""pmuludq  %%mm0,     %%mm2      \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""movd     20(%%esi), %%mm4      \n\t""   \
    ""pmuludq  %%mm0,     %%mm4      \n\t""   \
    ""paddq    %%mm3,     %%mm1      \n\t""   \
    ""movd     24(%%esi), %%mm6      \n\t""   \
    ""pmuludq  %%mm0,     %%mm6      \n\t""   \
    ""movd     %%mm1,     4(%%edi)   \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""movd     28(%%esi), %%mm3      \n\t""   \
    ""pmuludq  %%mm0,     %%mm3      \n\t""   \
    ""paddq    %%mm5,     %%mm1      \n\t""   \
    ""movd     16(%%edi), %%mm5      \n\t""   \
    ""paddq    %%mm5,     %%mm2      \n\t""   \
    ""movd     %%mm1,     8(%%edi)   \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""paddq    %%mm7,     %%mm1      \n\t""   \
    ""movd     20(%%edi), %%mm5      \n\t""   \
    ""paddq    %%mm5,     %%mm4      \n\t""   \
    ""movd     %%mm1,     12(%%edi)  \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""paddq    %%mm2,     %%mm1      \n\t""   \
    ""movd     24(%%edi), %%mm5      \n\t""   \
    ""paddq    %%mm5,     %%mm6      \n\t""   \
    ""movd     %%mm1,     16(%%edi)  \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""paddq    %%mm4,     %%mm1      \n\t""   \
    ""movd     28(%%edi), %%mm5      \n\t""   \
    ""paddq    %%mm5,     %%mm3      \n\t""   \
    ""movd     %%mm1,     20(%%edi)  \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""paddq    %%mm6,     %%mm1      \n\t""   \
    ""movd     %%mm1,     24(%%edi)  \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""paddq    %%mm3,     %%mm1      \n\t""   \
    ""movd     %%mm1,     28(%%edi)  \n\t""   \
    ""addl     $32,       %%edi      \n\t""   \
    ""addl     $32,       %%esi      \n\t""   \
    ""psrlq    $32,       %%mm1      \n\t""   \
    ""movd     %%mm1,     %%ecx      \n\t""

".SplitNewLine();

    // complex
    [Fact]
    public void ComplexReaderTest()
    {
        var reader = new SymbolReader(new SymbolReaderOption(DistinctSymbol: false));

        // externField
        {
            var actual = reader.Read(DetectionType.ExternField, Contents, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(6);
        }

        // method
        {
            var actual = reader.Read(DetectionType.Method, Contents, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(8);
        }

        // typedef
        {
            var actual = reader.Read(DetectionType.Typedef, Contents, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(9);
        }
    }

    [Fact]
    public void ComplexReaderDistinctTest()
    {
        var reader = new SymbolReader(new SymbolReaderOption(DistinctSymbol: true));

        // externField
        {
            var actual = reader.Read(DetectionType.ExternField, Contents, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(6);
        }

        // method
        {
            var actual = reader.Read(DetectionType.Method, Contents, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(7);
        }

        // typedef
        {
            var actual = reader.Read(DetectionType.Typedef, Contents, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(9);
        }
    }

    // extern field
    [Theory]
    [InlineData(@"extern const foo_info_t foo_info;", "foo_info", PREFIX + "foo_info")]
    [InlineData(@"  extern int bar_int;", "bar_int", PREFIX + "bar_int")]
    [InlineData(@"extern void (*function_pointer_foo)( const char * test, int line, const char * file );", "function_pointer_foo", PREFIX + "function_pointer_foo")]
    [InlineData(@"  extern int ( *function_pointer_bar )();", "function_pointer_bar", PREFIX + "function_pointer_bar")]
    [InlineData(@"extern void* (function_pointer_piyo)( );", "function_pointer_piyo", PREFIX + "function_pointer_piyo")]
    [InlineData(@"extern FStar_UInt128_uint128
        (*FStar_UInt128_op_Greater_Greater_Hat)(FStar_UInt128_uint128 x0, uint32_t x1);", "FStar_UInt128_op_Greater_Greater_Hat", PREFIX + "FStar_UInt128_op_Greater_Greater_Hat")]
    public void ExternFieldReaderTest(string define, string expectedSymbol, string expectedRenamedSymbol)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.ExternField, content, s => PREFIX + s);

        actuals.Should().NotBeEmpty();
        foreach (var actual in actuals)
        {
            actual.Should().NotBeNull();
            actual!.Symbol.Should().Be(expectedSymbol);
            actual!.RenamedSymbol.Should().Be(expectedRenamedSymbol);
            actual!.Delimiters!.Should().Equal(new[] { ";"});
        }
    }

    [Theory]
    [InlineData(@"// extern const int comment_out_field;")]
    [InlineData(@"//  extern int bar_int;")]
    public void ExternFieldReaderCommentTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.ExternField, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"extern const int invalid_field")]
    [InlineData(@"extern void* (invalid_function)( )")]
    [InlineData(@"externextern const foo_info_t do_not_read;")]
    [InlineData(@"externextern void* (do_not_read_function)( );")]
    public void ExternFieldReaderInvalidTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.ExternField, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"typedef uint64_t mbedtls_mpi_uint;")]
    [InlineData(@"  typedef uint64_t piyopiyo;")]
    [InlineData(@"typedef enum {
            MBEDTLS_SSL_MODE_STREAM = 0,
            MBEDTLS_SSL_MODE_CBC,
            MBEDTLS_SSL_MODE_CBC_ETM,
            MBEDTLS_SSL_MODE_AEAD
        } mbedtls_ssl_mode_t;")]
    [InlineData(@"typedef struct
    {
        struct key_data
        {
            uint8_t *data;
            size_t bytes;
        } key;
    } psa_key_slot_t;")]
    public void ExternFieldCannotReadTypedefTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.ExternField, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"void foo();")]
    [InlineData(@"   int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );")]
    [InlineData(@"FIBOLIB_API void natoka_hogemoge(sample_data_t *output);")]
    [InlineData(@"  FIBOLIB_API int fugafuga(int n);")]
    [InlineData(@"mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(;        ")]
    [InlineData(@"    mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,")]
    public void ExternFieldReaderCannotReadMethodTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.ExternField, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    // method
    [Theory]
    [InlineData(@"void foo();", "foo", PREFIX + "foo")]
    [InlineData(@"   int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );", "mbedtls_ssl_write_client_hello", PREFIX + "mbedtls_ssl_write_client_hello")]
    [InlineData(@"FIBOLIB_API void natoka_hogemoge(sample_data_t *output);", "natoka_hogemoge", PREFIX + "natoka_hogemoge")]
    [InlineData(@"  FIBOLIB_API int fugafuga(int n);", "fugafuga", PREFIX + "fugafuga")]
    [InlineData(@"mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform();        ", "mbedtls_ssl_get_mode_from_transform", PREFIX + "mbedtls_ssl_get_mode_from_transform")]
    [InlineData(@"    mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,
                                           const mbedtls_mpi_uint *s, size_t s_len,
                                           mbedtls_mpi_uint b );        ", "mbedtls_mpi_core_mla", PREFIX + "mbedtls_mpi_core_mla")]
    public void MethodReaderTest(string define, string expectedSymbol, string expectedRenamedSymbol)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().NotBeEmpty();
        foreach (var actual in actuals)
        {
            actual.Should().NotBeNull();
            actual!.Symbol.Should().Be(expectedSymbol);
            actual!.RenamedSymbol.Should().Be(expectedRenamedSymbol);
            actual!.Delimiters!.Should().Equal(new[] { "(" });
        }
    }

    [Theory]
    [InlineData(@"// void should_be_ignopre_method();")]
    [InlineData(@"// FIBOLIB_API void should_be_ignopre_method(sample_data_t *output);")]
    [InlineData(@"// mbedtls_ssl_mode_t should_be_ignopre_method(
                const mbedtls_ssl_transform *transform );")]
    [InlineData(@"/*
     * GCM multiplication: c = a times b in GF(2^128)
     * Based on [CLMUL-WP] algorithms 1 (with equation 27) and 5.
     */")]
    public void MethodReaderCommentTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"const mbedtls_ssl_transform *transform )")]
    [InlineData(@"constconst mbedtls_mpi_uint *s, size_t s_len,")]
    [InlineData(@"const                                          mbedtls_mpi_uint b );        ")]
    [InlineData(@"#ifndef FIBOLIB_VISIBILITY")]
    [InlineData(@"#  if defined(__GNUC__) && (__GNUC__ >= 4)")]
    [InlineData(@"#    define FIBOLIB_VISIBILITY __attribute__ ((visibility (""default"")))")]
    [InlineData(@"#  else")]
    [InlineData(@"#    define FIBOLIB_VISIBILITY")]
    [InlineData(@"#  endif")]
    [InlineData(@"#endif")]
    public void MethodReaderInvalidTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"typedef uint64_t mbedtls_mpi_uint;")]
    [InlineData(@"  typedef uint64_t piyopiyo;")]
    [InlineData(@"typedef enum {
        MBEDTLS_SSL_MODE_STREAM = 0,
        MBEDTLS_SSL_MODE_CBC,
        MBEDTLS_SSL_MODE_CBC_ETM,
        MBEDTLS_SSL_MODE_AEAD
    } mbedtls_ssl_mode_t;")]
    [InlineData(@"typedef struct
    {
        struct key_data
        {
            uint8_t *data;
            size_t bytes;
        } key;
    } psa_key_slot_t;")]
    [InlineData(@"typedef struct
{
    foo_t struct_method(alg);
    union
    {
        unsigned struct_method(dummy); /* Make the union non-empty even with no supported algorithms. */
#if defined(PIYO) || defined(PIYO_TEST)
        foo_t struct_method(hmac);
#endif /* PIYO */
#if defined(PIYO_FOO) || defined(PIYO_TEST)
        bar_t struct_method(cmac);
#endif /* PIYO_FOO */
    } struct_method(ctx);
} include_method_t;")]
    public void MethodReaderCannotReadTypedefTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"#define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h ) \
        MBEDTLS_BYTES_TO_T_UINT_4( a, b, c, d ),                \
        MBEDTLS_BYTES_TO_T_UINT_4( e, f, g, h )")]
    [InlineData(@"#define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h )   \
        ( (mbedtls_mpi_uint) (a) <<  0 ) |                        \
        ( (mbedtls_mpi_uint) (b) <<  8 ) |                        \
        ( (mbedtls_mpi_uint) (c) << 16 ) |                        \
        ( (mbedtls_mpi_uint) (d) << 24 ) |                        \
        ( (mbedtls_mpi_uint) (e) << 32 ) |                        \
        ( (mbedtls_mpi_uint) (f) << 40 ) |                        \
        ( (mbedtls_mpi_uint) (g) << 48 ) |                        \
        ( (mbedtls_mpi_uint) (h) << 56 )")]
    [InlineData(@"    #define MULADDC_X8_CORE                         \
        ""movd     %%ecx,     %%mm1      \n\t""   \
        ""movd     %%ebx,     %%mm0      \n\t""   \
        ""movd     (%%edi),   %%mm3      \n\t""   \
        ""paddq    %%mm3,     %%mm1      \n\t""   \
        ""movd     (%%esi),   %%mm2      \n\t""   \
        ""pmuludq  %%mm0,     %%mm2      \n\t""   \
        ""movd     4(%%esi),  %%mm4      \n\t""   \
        ""pmuludq  %%mm0,     %%mm4      \n\t""   \
        ""movd     8(%%esi),  %%mm6      \n\t""   \
        ""pmuludq  %%mm0,     %%mm6      \n\t""   \
        ""movd     12(%%esi), %%mm7      \n\t""   \
        ""pmuludq  %%mm0,     %%mm7      \n\t""   \
        ""paddq    %%mm2,     %%mm1      \n\t""   \
        ""movd     4(%%edi),  %%mm3      \n\t""   \
        ""paddq    %%mm4,     %%mm3      \n\t""   \
        ""movd     8(%%edi),  %%mm5      \n\t""   \
        ""paddq    %%mm6,     %%mm5      \n\t""   \
        ""movd     12(%%edi), %%mm4      \n\t""   \
        ""paddq    %%mm4,     %%mm7      \n\t""   \
        ""movd     %%mm1,     (%%edi)    \n\t""   \
        ""movd     16(%%esi), %%mm2      \n\t""   \
        ""pmuludq  %%mm0,     %%mm2      \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""movd     20(%%esi), %%mm4      \n\t""   \
        ""pmuludq  %%mm0,     %%mm4      \n\t""   \
        ""paddq    %%mm3,     %%mm1      \n\t""   \
        ""movd     24(%%esi), %%mm6      \n\t""   \
        ""pmuludq  %%mm0,     %%mm6      \n\t""   \
        ""movd     %%mm1,     4(%%edi)   \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""movd     28(%%esi), %%mm3      \n\t""   \
        ""pmuludq  %%mm0,     %%mm3      \n\t""   \
        ""paddq    %%mm5,     %%mm1      \n\t""   \
        ""movd     16(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm2      \n\t""   \
        ""movd     %%mm1,     8(%%edi)   \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm7,     %%mm1      \n\t""   \
        ""movd     20(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm4      \n\t""   \
        ""movd     %%mm1,     12(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm2,     %%mm1      \n\t""   \
        ""movd     24(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm6      \n\t""   \
        ""movd     %%mm1,     16(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm4,     %%mm1      \n\t""   \
        ""movd     28(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm3      \n\t""   \
        ""movd     %%mm1,     20(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm6,     %%mm1      \n\t""   \
        ""movd     %%mm1,     24(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm3,     %%mm1      \n\t""   \
        ""movd     %%mm1,     28(%%edi)  \n\t""   \
        ""addl     $32,       %%edi      \n\t""   \
        ""addl     $32,       %%esi      \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""movd     %%mm1,     %%ecx      \n\t""")]
    public void MethodReaderCannotReadDefineTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"struct foo
    {
        bar *next;  /*!< next handshake message(s)              */
    };")]
    public void MethodReaderCannotReadStructTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"static inline int mbedtls_ssl_get_psk( const mbedtls_ssl_context *ssl,
        const unsigned char **psk, size_t *psk_len )
    {
        if( ssl->handshake->psk != NULL && ssl->handshake->psk_len > 0 )
        {
            *psk = ssl->handshake->psk;
            *psk_len = ssl->handshake->psk_len;
        }

        else if( ssl->conf->psk != NULL && ssl->conf->psk_len > 0 )
        {
            *psk = ssl->conf->psk;
            *psk_len = ssl->conf->psk_len;
        }

        else
        {
            *psk = NULL;
            *psk_len = 0;
            return( MBEDTLS_ERR_SSL_PRIVATE_KEY_REQUIRED );
        }

        return( 0 );
    }")]
    public void MethodReaderCannotReadStaticInlineTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    // typdef
    [Theory]
    [InlineData(@"typedef uint64_t simple_t;", "simple_t", PREFIX + "simple_t")]
    [InlineData(@"  typedef uint64_t simple_space_t;", "simple_space_t", PREFIX + "simple_space_t")]
    [InlineData(@"typedef enum {
        MBEDTLS_SSL_MODE_STREAM = 0,
        MBEDTLS_SSL_MODE_CBC,
        MBEDTLS_SSL_MODE_CBC_ETM,
        MBEDTLS_SSL_MODE_AEAD
    } enum_t;", "enum_t", PREFIX + "enum_t")]
    [InlineData(@"typedef struct mbedtls_aes_contextZ
    {
        int MBEDTLS_PRIVATE(nr);                     /*!< The number of rounds. */
        uint32_t *MBEDTLS_PRIVATE(rk);               /*!< AES round keys. */
        uint32_t MBEDTLS_PRIVATE(buf)[68];           /*!< Unaligned data buffer. This buffer can
                                            hold 32 extra Bytes, which can be used for
                                            one of the following purposes:
                                            <ul><li>Alignment if VIA padlock is
                                                    used.</li>
                                            <li>Simplifying key expansion in the 256-bit
                                                case by generating an extra round key.
                                                </li></ul> */
    }
    multiline_next_t;", "multiline_next_t", PREFIX + "multiline_next_t")]
    [InlineData(@"typedef struct mbedtls_aes_xts_context
    {
        mbedtls_aes_context MBEDTLS_PRIVATE(crypt); /*!< The AES context to use for AES block
                                            encryption or decryption. */
        mbedtls_aes_context MBEDTLS_PRIVATE(tweak); /*!< The AES context used for tweak
                                            computation. */
    } multiline_t;", "multiline_t", PREFIX + "multiline_t")]
    [InlineData(@"typedef struct
    {
        struct key_data
        {
            uint8_t *data;
            size_t bytes;
        } inside;
    } nested_t;", "nested_t", PREFIX + "nested_t")]
    [InlineData(@"    typedef struct
        {
            void *key;
            mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
            mbedtls_pk_rsa_alt_sign_func sign_func;
            mbedtls_pk_rsa_alt_key_len_func key_len_func;
        } spaces_multiline_t;", "spaces_multiline_t", PREFIX + "spaces_multiline_t")]
    [InlineData("typedef struct singleline_typedef_struct_s singleline_typedef_struct_t;", "singleline_typedef_struct_t", PREFIX + "singleline_typedef_struct_t")]
    public void TypedefReaderTest(string define, string expectedSymbol, string expectedRenamedSymbol)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().NotBeEmpty();
        foreach (var actual in actuals)
        {
            actual.Should().NotBeNull();
            actual!.Symbol.Should().Be(expectedSymbol);
            actual!.RenamedSymbol.Should().Be(expectedRenamedSymbol);
            actual!.Delimiters!.Should().Equal(new[] { ";", " " });
        }
    }

    [Theory]
    [InlineData(@"//  typedef uint64_t should_be_ignopre_typdef;")]
    [InlineData(@"// typedef enum {
            MBEDTLS_SSL_MODE_STREAM = 0,
            MBEDTLS_SSL_MODE_CBC,
            MBEDTLS_SSL_MODE_CBC_ETM,
            MBEDTLS_SSL_MODE_AEAD
        } should_be_ignopre_typdef;")]
    [InlineData(@"// typedef enum {
            // MBEDTLS_SSL_MODE_STREAM = 0,
        // } should_be_ignopre_typdef;")]
    public void TypedefReaderCommentTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"typedef uint64_t mbedtls_mpi_uint")] // missing end semi-colon
    [InlineData(@"typedef enum {
            MBEDTLS_SSL_MODE_STREAM = 0,
            MBEDTLS_SSL_MODE_CBC,
            MBEDTLS_SSL_MODE_CBC_ETM,
            MBEDTLS_SSL_MODE_AEAD
        } mbedtls_ssl_mode_t")] // missing end semi-colon
    [InlineData(@"typede uint64_t mbedtls_mpi_uint;")] // typede
    public void TypedefReaderInvalidTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"void foo();")]
    [InlineData(@"   int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );")]
    [InlineData(@"FIBOLIB_API void natoka_hogemoge(sample_data_t *output);")]
    [InlineData(@"  FIBOLIB_API int fugafuga(int n);")]
    [InlineData(@"mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(;        ")]
    [InlineData(@"    mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,")]
    public void TypedefReaderCannotReadMethodTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"#define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h ) \
        MBEDTLS_BYTES_TO_T_UINT_4( a, b, c, d ),                \
        MBEDTLS_BYTES_TO_T_UINT_4( e, f, g, h )")]
    [InlineData(@"#define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h )   \
        ( (mbedtls_mpi_uint) (a) <<  0 ) |                        \
        ( (mbedtls_mpi_uint) (b) <<  8 ) |                        \
        ( (mbedtls_mpi_uint) (c) << 16 ) |                        \
        ( (mbedtls_mpi_uint) (d) << 24 ) |                        \
        ( (mbedtls_mpi_uint) (e) << 32 ) |                        \
        ( (mbedtls_mpi_uint) (f) << 40 ) |                        \
        ( (mbedtls_mpi_uint) (g) << 48 ) |                        \
        ( (mbedtls_mpi_uint) (h) << 56 )")]
    [InlineData(@"    #define MULADDC_X8_CORE                         \
        ""movd     %%ecx,     %%mm1      \n\t""   \
        ""movd     %%ebx,     %%mm0      \n\t""   \
        ""movd     (%%edi),   %%mm3      \n\t""   \
        ""paddq    %%mm3,     %%mm1      \n\t""   \
        ""movd     (%%esi),   %%mm2      \n\t""   \
        ""pmuludq  %%mm0,     %%mm2      \n\t""   \
        ""movd     4(%%esi),  %%mm4      \n\t""   \
        ""pmuludq  %%mm0,     %%mm4      \n\t""   \
        ""movd     8(%%esi),  %%mm6      \n\t""   \
        ""pmuludq  %%mm0,     %%mm6      \n\t""   \
        ""movd     12(%%esi), %%mm7      \n\t""   \
        ""pmuludq  %%mm0,     %%mm7      \n\t""   \
        ""paddq    %%mm2,     %%mm1      \n\t""   \
        ""movd     4(%%edi),  %%mm3      \n\t""   \
        ""paddq    %%mm4,     %%mm3      \n\t""   \
        ""movd     8(%%edi),  %%mm5      \n\t""   \
        ""paddq    %%mm6,     %%mm5      \n\t""   \
        ""movd     12(%%edi), %%mm4      \n\t""   \
        ""paddq    %%mm4,     %%mm7      \n\t""   \
        ""movd     %%mm1,     (%%edi)    \n\t""   \
        ""movd     16(%%esi), %%mm2      \n\t""   \
        ""pmuludq  %%mm0,     %%mm2      \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""movd     20(%%esi), %%mm4      \n\t""   \
        ""pmuludq  %%mm0,     %%mm4      \n\t""   \
        ""paddq    %%mm3,     %%mm1      \n\t""   \
        ""movd     24(%%esi), %%mm6      \n\t""   \
        ""pmuludq  %%mm0,     %%mm6      \n\t""   \
        ""movd     %%mm1,     4(%%edi)   \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""movd     28(%%esi), %%mm3      \n\t""   \
        ""pmuludq  %%mm0,     %%mm3      \n\t""   \
        ""paddq    %%mm5,     %%mm1      \n\t""   \
        ""movd     16(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm2      \n\t""   \
        ""movd     %%mm1,     8(%%edi)   \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm7,     %%mm1      \n\t""   \
        ""movd     20(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm4      \n\t""   \
        ""movd     %%mm1,     12(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm2,     %%mm1      \n\t""   \
        ""movd     24(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm6      \n\t""   \
        ""movd     %%mm1,     16(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm4,     %%mm1      \n\t""   \
        ""movd     28(%%edi), %%mm5      \n\t""   \
        ""paddq    %%mm5,     %%mm3      \n\t""   \
        ""movd     %%mm1,     20(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm6,     %%mm1      \n\t""   \
        ""movd     %%mm1,     24(%%edi)  \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""paddq    %%mm3,     %%mm1      \n\t""   \
        ""movd     %%mm1,     28(%%edi)  \n\t""   \
        ""addl     $32,       %%edi      \n\t""   \
        ""addl     $32,       %%esi      \n\t""   \
        ""psrlq    $32,       %%mm1      \n\t""   \
        ""movd     %%mm1,     %%ecx      \n\t""")]
    public void TypedefReaderCannotReadDefineTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }
}
