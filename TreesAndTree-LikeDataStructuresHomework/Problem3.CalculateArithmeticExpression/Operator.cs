namespace Problem3.CalculateArithmeticExpression
{
    public class Operator
    {
        private string signStringRepresentation;
        public Operator(string sign)
        {
            if (sign == "^")
            {
                this.signStringRepresentation = "^";
                this.Precedence = 4;
                this.IsRightAssociative = true;
            }
            else if (sign == "*" || sign == "/")
            {
                this.signStringRepresentation = sign;
                this.Precedence = 3;
                this.IsLeftAssociative = true;
            }
            else if (sign == "+" || sign == "-")
            {
                this.signStringRepresentation = sign;
                this.Precedence = 2;
                this.IsLeftAssociative = true;
            }
        }

        public bool IsRightAssociative { get; set; }

        public bool IsLeftAssociative { get; set; }

        public int Precedence { get; set; }
    }
}