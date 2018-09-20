namespace TellDontAskKata.UseCase
{
    /// <remarks>
    /// I'd also be tempted to redesign this, the next time I had to touch
    /// it. Rejecting an Order by submitting an ApprovalRequest with
    /// Approved = false is needlessly comlex.
    ///
    /// A separate OrderRejectionUseCase with an OrderRejectionRequest
    /// would make more sense.
    /// </remarks>
    public class OrderApprovalRequest
    {
        public int OrderId { get; set; }
        public bool Approved { get; set; }
    }
}