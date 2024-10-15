namespace Domain.Entities;
public class User
{
    public string Id { get; private set; } // Private set ensures encapsulation
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool ConsentToDataCollection { get; private set; }

    // Constructor to create a valid User object
    public User(string name, string email, bool consentToDataCollection)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        ConsentToDataCollection = consentToDataCollection;
        CreatedAt = DateTime.UtcNow;

        if (!ConsentToDataCollection)
        {
            throw new InvalidOperationException("Consent to data collection is required.");
        }
    }

    // Business logic to change user consent (GDPR compliance)
    public void UpdateConsent(bool consent)
    {
        ConsentToDataCollection = consent;
    }
}