using EncurtadorURLs.Data;
using EncurtadorURLs.Models;


namespace EncurtadorURLs.Services;


/// <summary>
/// Interface para o serviço de URLs. Define as operações de negócio disponíveis.
/// </summary>
public interface IUrlsService
{
    Task<UrlItem> CreateShortUrlAsync(string originalUrl);


    Task<UrlItem?> GetByIdAsync(Guid id);
}


/// <summary>
/// Serviço de regras de negócio para gerenciamento de URLs encurtadas.
/// </summary>
public class UrlsService : IUrlsService
{
    private readonly IUrlContext _context;


    private const int MaxShortCodeRetries = 5;
    private const int MinShortCodeLength = 6;
    private const int MaxShortCodeLength = 8;
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";


    private readonly Random _random = new();


    public UrlsService(IUrlContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    /// <summary>
    /// Cria um novo registro de URL encurtada.
    /// </summary>
    /// <remarks>
    /// IMPLEMENTAÇÃO PENDENTE (fase RED do TDD) — será implementada na próxima aula.
    /// </remarks>
    public async Task<UrlItem> CreateShortUrlAsync(string originalUrl)
    {
        if (string.IsNullOrWhiteSpace(originalUrl))
            throw new ArgumentException("URL original não pode ser vazia.", nameof(originalUrl));


        if (!IsValidUrl(originalUrl))
            throw new ArgumentException("URL original possui formato inválido. Deve iniciar com http:// ou https://", nameof(originalUrl));


        if (originalUrl.Length > 2048)
            throw new ArgumentException("URL original não pode exceder 2048 caracteres.", nameof(originalUrl));


        var shortCode = await GenerateUniqueShortCodeAsync();
        var urlItem = new UrlItem
        {
            Id = Guid.NewGuid(),
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            CreatedAt = DateTimeOffset.UtcNow,
            ClickCount = 0
        };


        await _context.AddAsync(urlItem);
        return urlItem;
    }
    public Task<UrlItem?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task<UrlItem> CreateShortUrlAsync(string originalUrl)
    {
        if (string.IsNullOrWhiteSpace(originalUrl))
            throw new ArgumentException("URL original não pode ser vazia.", nameof(originalUrl));

        if (!IsValidUrl(originalUrl))
            throw new ArgumentException("URL original possui formato inválido. Deve iniciar com http:// ou https://", nameof(originalUrl));

        if (originalUrl.Length > 2048)
            throw new ArgumentException("URL original não pode exceder 2048 caracteres.", nameof(originalUrl));

        var shortCode = await GenerateUniqueShortCodeAsync();
        var urlItem = new UrlItem
        {
            Id = Guid.NewGuid(),
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            CreatedAt = DateTimeOffset.UtcNow,
            ClickCount = 0
        };

        await _context.AddAsync(urlItem);
        return urlItem;
    }
}