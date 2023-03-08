namespace Birdy.Shared;

public static class LoginDataExtension
{
    public static bool LoginDataEquals(this LoginData lg1, LoginData lg2)
    {
        return (
            (((lg1.Email is null) && (lg2.Email is null)) ? true :
            (((lg1.Email is null) && (lg2.Email is not null)) || ((lg1.Email is not null) && (lg2.Email is null))) ? false :
            lg1.Email!.Equals(lg2.Email, StringComparison.OrdinalIgnoreCase) ? true : false) &&
            (((lg1.Password is null) && (lg2.Password is null)) ? true :
            (((lg1.Password is null) && (lg2.Password is not null)) || ((lg1.Password is not null) && (lg2.Password is null))) ? false :
            lg1.Password!.Equals(lg2.Password) ? true : false)
            );
    }

    public static bool LoginDataEqualsNoPassword(this LoginData lg1, LoginData lg2)
    {
        /*Пароли не сравнивать?*/
        return (
            (((lg1.Email is null) && (lg2.Email is null)) ? true :
            (((lg1.Email is null) && (lg2.Email is not null)) || ((lg1.Email is not null) && (lg2.Email is null))) ? false :
            lg1.Email!.Equals(lg2.Email, StringComparison.OrdinalIgnoreCase) ? true : false)
            );
    }
}
