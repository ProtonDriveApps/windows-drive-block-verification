using System.Text.Json.Serialization;

namespace ProtonDrive.BlockVerification;

internal sealed record VerificationInputResponse
{
    [JsonConverter(typeof(Base64JsonConverter))]
    public ReadOnlyMemory<byte> VerificationCode { get; init; }

    [JsonConverter(typeof(Base64JsonConverter))]
    public ReadOnlyMemory<byte> ContentKeyPacket { get; init; }
}
