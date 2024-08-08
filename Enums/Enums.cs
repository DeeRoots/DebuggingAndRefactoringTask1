namespace DebuggingAndRefactoringTask1.Enums
{
    public static class Enums
    {
        //MessageType for Message Bus
        public enum MessageType
        {
            Information = 0,
            Success = 1,
            Warning = 2,
            Error = 3,
            MenuItem = 4,
            Input = 5

        }

        //AccountInteractionType for Monetary interactions.
        public enum AccountInteractionType
        {
            Deposit = 0,
            Withdraw = 1,
            Tranfer = 2
        }
    }
}
