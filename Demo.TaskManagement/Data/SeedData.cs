using Demo.TaskManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.TaskManagement.Data
{
    public static class SeedData
    {
        public static async System.Threading.Tasks.Task Initialize(ApplicationDbContext db, UserManager<User> userManager)
        {
            if ((await db.Accounts.FirstOrDefaultAsync(a => a.Title == "Elegis s.r.o")) == null)
            {
                await db.Accounts.AddAsync(new Account()
                {
                    Title = "Elegis s.r.o",
                    TAXNumber = "26927233",
                    VATNumber = "CZ26927233"
                });
            }
            if ((await db.Accounts.FirstOrDefaultAsync(a => a.Title == "KALÁB - develop, s.r.o.")) == null)
            {
                await db.Accounts.AddAsync(new Account()
                {
                    Title = "KALÁB - develop, s.r.o.",
                    TAXNumber = "26928981"
                });
            }
            if ((await db.Accounts.FirstOrDefaultAsync(a => a.Title == "OHLA ŽS, a.s.")) == null)
            {
                await db.Accounts.AddAsync(new Account()
                {
                    Title = "OHLA ŽS, a.s.",
                    TAXNumber = "46342796",
                    VATNumber = "CZ46342796"
                });
            }
            await db.SaveChangesAsync();

            var account = await db.Accounts.FirstOrDefaultAsync(a => a.Title == "Elegis s.r.o");
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == "michal.ondrak@elegis.cz");
            var tester = await db.Users.FirstOrDefaultAsync(u => u.UserName == "tester@elegis.cz");

            if (account != null && user != null && tester != null)
            {
                if ((await db.Tasks.FirstOrDefaultAsync(a => a.Title == "Vytvoření inzerátu na nového vývojáře")) == null)
                {
                    await db.Tasks.AddAsync(new Entities.Task
                    {
                        Title = "Vytvoření inzerátu na nového vývojáře",
                        Description = "Vytvoření inzerátu a rozeslání na portály",
                        CreatedById = user.Id,
                        AssignedToId = tester.Id,
                        Priority = Enums.TaskPriority.High,
                        State = Enums.TaskState.Finished,
                        AccountId = account.Id,
                        CreatedOn = new DateTime(2023, 10, 25),
                        Deadline = new DateTime(2023, 11, 15),
                        Solved = new DateTime(2023, 11, 5),
                        Messages = new List<Message>
                        {
                            new Message
                            {
                                Note = "Inzerát mám připravenej, můžeš ho prosím zkontrolovat?",
                                UserId = user.Id,
                                Created = new DateTime(2023, 11, 11),
                            },
                            new Message
                            {
                                Note = "V pořádku, můžeš rozeslat",
                                UserId = tester.Id,
                                Created = new DateTime(2023, 11, 13)
                            }
                        },
                        CheckListSteps = new List<CheckListStep>
                        {
                            new CheckListStep
                            {
                                Title = "Vytvoření inzerátu",
                                Deadline = new DateTime(2023, 11, 12),
                                Finished = true
                            },
                            new CheckListStep
                            {
                                Title = "Kontrola inzerátu",
                                Deadline = new DateTime(2023, 11, 13),
                                Finished = true
                            },
                            new CheckListStep
                            {
                                Title = "Rozeslání inzerátu",
                                Deadline = new DateTime(2023, 11, 15),
                                Finished = true
                            }
                        }
                    });
                }

                if ((await db.Tasks.FirstOrDefaultAsync(a => a.Title == "Vytvoření zadání úkolu pro uchazeče")) == null)
                {
                    await db.Tasks.AddAsync(new Entities.Task()
                    {
                        Title = "Vytvoření zadání úkolu pro uchazeče",
                        Description = "Nějaké praktické zadání které ukáže potencionál uchazeče",
                        CreatedById = user.Id,
                        AssignedToId = user.Id,
                        Priority = Enums.TaskPriority.Normal,
                        State = Enums.TaskState.Finished,
                        AccountId = account.Id,
                        CreatedOn = new DateTime(2023, 10, 25),
                        Deadline = new DateTime(2023, 12, 1),
                        Solved = new DateTime(2023, 11, 28),
                        CheckListSteps = new List<CheckListStep>
                        {
                            new CheckListStep
                            {
                                Title = "Analýza možných scénářů",
                                Deadline = new DateTime(2023, 11, 20),
                                Finished = true
                            },
                            new CheckListStep
                            {
                                Title = "Výběr scénáře",
                                Deadline = new DateTime(2023, 11, 25),
                                Finished = true
                            },
                            new CheckListStep
                            {
                                Title = "Potvrzení ředitelem",
                                Deadline = new DateTime(2023, 12, 1),
                                Finished = true
                            }
                        }
                    });
                }

                if ((await db.Tasks.FirstOrDefaultAsync(a => a.Title == "Výběr nového kolegy")) == null)
                {
                    await db.Tasks.AddAsync(new Entities.Task()
                    {
                        Title = "Výběr nového kolegy",
                        Description = "Na základě schůzek, splněného úkolu a osobního pocitu vybrat vhodného kolegu.",
                        CreatedById = user.Id,
                        AssignedToId = user.Id,
                        Priority = Enums.TaskPriority.Normal,
                        State = Enums.TaskState.Processing,
                        AccountId = account.Id,
                        CreatedOn = new DateTime(2023, 10, 25),
                        Deadline = new DateTime(2024, 1, 31),
                        CheckListSteps = new List<CheckListStep>
                        {
                            new CheckListStep
                            {
                                Title = "Analýza dodaných úkolů",
                                Deadline = new DateTime(2024, 1, 20),
                                Finished = true
                            },
                            new CheckListStep
                            {
                                Title = "Výběr nejlepšího kandidáta (ehm Jiří Klíč ehm :D)",
                                Deadline = new DateTime(2024, 1, 25),
                                Finished = false
                            },
                            new CheckListStep
                            {
                                Title = "Notifikace nového kolegy",
                                Deadline = new DateTime(2024, 1, 31),
                                Finished = false
                            }
                        }
                    });
                }

                await db.SaveChangesAsync();
            }
        }

        private static async System.Threading.Tasks.Task EnsureUser(UserManager<User> userManager, User user, string password, string role)
        {
            await userManager.CreateAsync(user);
            await userManager.AddPasswordAsync(user, password);
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
