using APPLICATION.Shared.Domain.Interfaces;

namespace APPLICATION.Shared.Domain.Options
{
    public class SecretKey : ISecretKey
    {
        private readonly string _path = "Content\\Archive.enc";

        public SecretKey()
        {
            //Key = File.ReadAllText(_path);
            Key = "6HnmvqCp9YOOWU/yBLevBRUyrz52Pb0ASBqKV2bBM2G3fXCEQEM4XpZFuaEbeLdDCQch4ZiVMwLj85PUgILS5EGhlcgntfR1OpEviaJ6ehUcsUZGFS6JANcYP6poV2a+DUaR54ti+vVYdPGfJYb6Q2iwWizL3kr1oDz7bzgkNBaGVNbwX92xfWRSUE7TrFqtwblrb3ZdZzy4Kjhp6F8RzylIm1sFh64CF0uPtXAnLGrpx21zSaddW8cZn8CEi7rDz1ShX1ynShvpm97JkXVQFcf2pRBxt5/ZoLB3jb94FjNOGNvFWaOnk7yrzdDDnMCj1N7BnvRDvCWB0YGldYOgqpNGO9IbQMoR97axo+RYtdkhRgp2lmBn9lNFPEMkNZYGyAsDXHg5lv8RcJZThrhGQHW+zu3bwcJYSt0Vr39RmdP/p1cfZ6aV2Qo383K+nUT6tJZ58sBJgq6vCr3/R6qnBmTLuAy3sCmdy4GuByI0zmETU1W7yfPVWfLQlf3DOfTn2UChWjx9r9FBkm/bNnoPB1hqHTKwrOqyzfYgTN2po+oHPGte6dH7CUJErkzSlhxgGuCTkT/sgZ7VFat8/nhfR3u5kqAZpDNrPpbhomGRnRi84y+dJ8cG4Yn4yiVV9fnsx1XJ+W6DsgrAxseefJHdUi0PNJWiMNTxZkR6BtqOz+QTbjaufv7NOyFVUHJktD7ZgR++dXNeIgHIUx+BXuAez9qd365bKuomIFcENYNIX3C4GFFxMbZ9R4czMlbLgqRiXXUFcm+pkdt+sOm29UooyJEIhqFyFTs9jV6eh/frObA9b6Y1SYzdZmpQgtN4VhQp9hq/F8v5yPF9Q0H0laTQ7QrG4q/UtQNAy1evLnZtWaze1nr+tx3cQKRi6NE6QBuVN2Z+5HK/MI0oXPvKQaqIiizGXrBHvDGs/89fjdyqiKcTyXvrQLHHpNLtYNShv7tVYkf9Ajr22Iql1YKaRPJw1WkOar0nOj04scBD9202FXc8k1scIm6OU9uA/5PlAsUnw73StOyJW+8ZEF+vCZ9sB+iAQkqrSHTdL9JU4YwXWuKvjb+U2lqxGnFNGjw5c5S8SxG1sYBDTRUqxET9Ec3/W7z3YoTKUHKcaSVpFKMoyc49Q5v5BkaFSedF5dG1tUZcT6eSMb1KPC2+aRKr5OdeU6AgaJiWkagMfmQTrVFEtRc=";
        }

        public string Key { get; set; }
    }
}