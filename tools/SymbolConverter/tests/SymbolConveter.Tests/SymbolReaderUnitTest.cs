namespace SymbolConveter.Tests;

public class SymbolReaderUnitTest
{
    const string PREFIX = "THISIS_PREFIX_";

    // complex
    [Fact]
    public void ReadTest()
    {
        var content = @"
typedef uint64_t mbedtls_mpi_uint;
    typedef uint64_t piyopiyo;
// typedef uint64_t should_be_ignopre_typdef;

typedef enum {
    MBEDTLS_SSL_MODE_STREAM = 0,
    MBEDTLS_SSL_MODE_CBC,
    MBEDTLS_SSL_MODE_CBC_ETM,
    MBEDTLS_SSL_MODE_AEAD
} mbedtls_ssl_mode_t;

    typedef struct
    {
        void *key;
        mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
        mbedtls_pk_rsa_alt_sign_func sign_func;
        mbedtls_pk_rsa_alt_key_len_func key_len_func;
    } mbedtls_rsa_alt_context;

// typedef enum {
    MBEDTLS_SSL_MODE_STREAM = 0,
    MBEDTLS_SSL_MODE_CBC,
    MBEDTLS_SSL_MODE_CBC_ETM,
    MBEDTLS_SSL_MODE_AEAD
} should_be_ignopre_typdef;

void foo();
    int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );

FIBOLIB_API void natoka_hogemoge(sample_data_t *output);
    FIBOLIB_API int fugafuga(int n);

mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(
        const mbedtls_ssl_transform *transform );        
    mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,
                                        const mbedtls_mpi_uint *s, size_t s_len,
                                        mbedtls_mpi_uint b );        
    struct foo
    {
        bar *next;  /*!< next handshake message(s)              */
    }

    static inline int mbedtls_ssl_get_psk( const mbedtls_ssl_context *ssl,
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
// mbedtls_ssl_mode_t should_be_ignopre_method(
        const mbedtls_ssl_transform *transform );

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

    #define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h ) \
        MBEDTLS_BYTES_TO_T_UINT_4( a, b, c, d ),                \
        MBEDTLS_BYTES_TO_T_UINT_4( e, f, g, h )

    #define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h )   \
        ( (mbedtls_mpi_uint) (a) <<  0 ) |                        \
        ( (mbedtls_mpi_uint) (b) <<  8 ) |                        \
        ( (mbedtls_mpi_uint) (c) << 16 ) |                        \
        ( (mbedtls_mpi_uint) (d) << 24 ) |                        \
        ( (mbedtls_mpi_uint) (e) << 32 ) |                        \
        ( (mbedtls_mpi_uint) (f) << 40 ) |                        \
        ( (mbedtls_mpi_uint) (g) << 48 ) |                        \
        ( (mbedtls_mpi_uint) (h) << 56 )

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

        var reader = new SymbolReader();

        // method
        {
            var actual = reader.Read(DetectionType.Method, content, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(6);
        }

        // typedef
        {
            var actual = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(4);
        }
    }

    // method
    [Theory]
    [InlineData(@"void foo();", "foo", PREFIX + "foo")]
    [InlineData(@"   int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );", "mbedtls_ssl_write_client_hello", PREFIX + "mbedtls_ssl_write_client_hello")]
    [InlineData(@"FIBOLIB_API void natoka_hogemoge(sample_data_t *output);", "natoka_hogemoge", PREFIX + "natoka_hogemoge")]
    [InlineData(@"  FIBOLIB_API int fugafuga(int n);", "fugafuga", PREFIX + "fugafuga")]
    [InlineData(@"mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(;        ", "mbedtls_ssl_get_mode_from_transform", PREFIX + "mbedtls_ssl_get_mode_from_transform")]
    [InlineData(@"    mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,", "mbedtls_mpi_core_mla", PREFIX + "mbedtls_mpi_core_mla")]
    public void ReadMethodTest(string define, string expectedSymbol, string expectedRenamedSymbol)
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
            actual!.Delimiter!.Should().Be("(");
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
    public void ReadMethodCommentTest(string define)
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
    public void ReadMethodInvalidTest(string define)
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
    public void ReadMethodCannotReadTypedefTest(string define)
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
    public void ReadMethodCannotReadDefineTest(string define)
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
    public void ReadMethodCannotReadStructTest(string define)
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
    public void ReadMethodCannotReadStaticInlineTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    // typdef
    [Theory]
    [InlineData(@"typedef uint64_t mbedtls_mpi_uint;", "mbedtls_mpi_uint", PREFIX + "mbedtls_mpi_uint")]
    [InlineData(@"  typedef uint64_t piyopiyo;", "piyopiyo", PREFIX + "piyopiyo")]
    [InlineData(@"typedef enum {
            MBEDTLS_SSL_MODE_STREAM = 0,
            MBEDTLS_SSL_MODE_CBC,
            MBEDTLS_SSL_MODE_CBC_ETM,
            MBEDTLS_SSL_MODE_AEAD
        } mbedtls_ssl_mode_t;", "mbedtls_ssl_mode_t", PREFIX + "mbedtls_ssl_mode_t")]
    [InlineData(@"    typedef struct
            {
                void *key;
                mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
                mbedtls_pk_rsa_alt_sign_func sign_func;
                mbedtls_pk_rsa_alt_key_len_func key_len_func;
            } mbedtls_rsa_alt_context;", "mbedtls_rsa_alt_context", PREFIX + "mbedtls_rsa_alt_context")]
    public void ReadTypedefTest(string define, string expectedSymbol, string expectedRenamedSymbol)
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
            actual!.Delimiter!.Should().Be(";");
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
    public void ReadTypedefCommentTest(string define)
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
    public void ReadTypedefInvalidTest(string define)
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
    public void ReadTypedefCannotReadMethodTest(string define)
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
    public void ReadTypedefCannotReadDefineTest(string define)
    {
        var content = define.SplitNewLine();
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }
}
