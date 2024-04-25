using MediatR;

namespace SchoolManagementSystemAPI.Services.Teacher.Idempotency
{
    public abstract record IdempotencyCommand(Guid RequestId): IRequest;
    //{
        //Guid RequestId { get; set; }
        //public IdempotencyCommand(Guid RequestId) { this.RequestId = RequestId; }
    //}
}
