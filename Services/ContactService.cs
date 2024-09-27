using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services;

public class ContactService
{
    private readonly ContactDbContext _contactDbContext;

    public ContactService(ContactDbContext contactDbContext)
    {
        _contactDbContext = contactDbContext;
    }

    public async Task CreateContactsAsync(List<Contact> contacts)
    {
        await _contactDbContext.AddRangeAsync(contacts);
        await _contactDbContext.SaveChangesAsync();
    }

    public List<Contact> GetContacts()
    {
        return _contactDbContext.Contacts.ToList();
    }

    public async Task UpdateContactAsync(int id, Contact contact)
    {
        contact.Id = id;

        _contactDbContext.Entry(contact).State = EntityState.Modified;
        await _contactDbContext.SaveChangesAsync();
    }

    public async Task DeleteContactAsyncById(int id)
    {
        var contactToDelete = await _contactDbContext.Contacts.SingleAsync(c => c.Id == id);
        _contactDbContext.Remove(contactToDelete);
        await _contactDbContext.SaveChangesAsync();
    }
}