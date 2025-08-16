using System.ComponentModel.DataAnnotations;

namespace LightCRM.API.Contracts
{
    // Response
    public record ClientResponse(
        Guid Id, string Name, [EmailAddress] string Email, string Phone, ICollection<OrderResponse> Orders
        );

    public record OrderResponse(
        Guid Id, Guid ClientId, DateTime CreatedAt, ICollection<OrderItemResponse> Items
        );

    public record OrderItemResponse(
        Guid Id, Guid OrderId, Guid ProductId, int Quantity
        );

    public record ProductResponse(
        Guid Id, string Name, decimal Price, int Stock
        );

    // Request
    public record ClientRequest(
        string Name, [EmailAddress] string Email, string Phone
        );

    // govno snizu
    public record OrderRequest(
        Guid ClientId, ICollection<OrderItemRequest> Items
        );

    public record OrderItemRequest(
        Guid ProductId, int Quantity
        );

    public record ProductRequest(
        string Name, decimal Price, int Stock
        );

    public record UserRequest(
        [EmailAddress] string Email, string Password
        );

    public record RefreshTokensRequest(
        string oldRefreshToken
        );
}
