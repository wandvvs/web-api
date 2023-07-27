using System.Diagnostics.CodeAnalysis;
public class UserService{
    private readonly MyDbContext _dbContext;
    public UserService(MyDbContext dbContext){
        _dbContext = dbContext;
    }
    public User Register(string login, string password){

        if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)){
            throw new ArgumentException("Заполните все поля формы");
        }
        
        if(login.Length < 2 || password.Length < 2){
            throw new ArgumentException("Логин или пароль не могут быть меньше двух символов");
        }

        if(_dbContext.users.Any(x => x.Login == login)){
            throw new ArgumentException("Пользователь с таким именем уже существует");
        }

        var user = new User {Login = login, Password = password};
        _dbContext.users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }
    public int GetIdByName(string login){
        var user = _dbContext.users.SingleOrDefault(u => u.Login == login);
        return user.Id;
    }
    public string GetNameById(int id){
        var user = _dbContext.users.SingleOrDefault(x=> x.Id == id);
        return user.Login;
    }
    public User NameChange(string login, string newLogin){
        if(string.IsNullOrEmpty(newLogin)) throw new ArgumentException("Заполните все поля");
        if(_dbContext.users.Any(r => r.Login == newLogin)) throw new ArgumentException("Пользователь с таким именем уже существует");

        var user = _dbContext.users.FirstOrDefault(r=>r.Login == login);

        user.Login = newLogin;
        _dbContext.SaveChanges();
        return user;
    }
    public User Delete(string login){
        var user = _dbContext.users.FirstOrDefault(x => x.Login == login);
        _dbContext.users.Remove(user);
        _dbContext.SaveChanges();
        return user;
    }
    public User Login(string login, string password){

        if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)){
            throw new ArgumentException("Заполните все поля формы");
        }
        
        var user = _dbContext.users.FirstOrDefault(x => x.Login == login && x.Password == password);

        if(user != null) return user;
        else throw new ArgumentException("Неверный логин или пароль");
    }
    public List<User> GetAllUsers()
    {
        var users = _dbContext.users.OrderBy(x=> x.Id).ToList();
        return users;
    }
    public bool isAdmin(string login){
        var user = _dbContext.users.FirstOrDefault(x => x.Login == login);
        return user?.isAdmin ?? false;
    }

}