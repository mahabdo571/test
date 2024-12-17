using System;

class Program
{
    [Flags] // يُستخدم لتمثيل القيم كأعلام متعددة
    enum Permissions
    {
        None = 0,           // بدون صلاحيات
        Read =1,      // 0001 = 1
        Write = 2,     // 0010 = 2
        Execute =4,   // 0100 = 4
        Delete = 8     // 1000 = 8
    }

    static void Main(string[] args)
    {

        Console.WriteLine(Directory.GetCurrentDirectory());
        //// تعيين صلاحيات أولية
        //Permissions userPermissions =(Permissions) 14;
        //Console.WriteLine($"first: {userPermissions}");

        //// إضافة صلاحية Execute
        //userPermissions |= Permissions.Execute;
        //Console.WriteLine($"after add  Execute: {userPermissions}");

        //// التحقق إذا كان لديه صلاحية Write
        //bool canWrite = ((Permissions)14 & Permissions.Write) == Permissions.Write;
        //Console.WriteLine($"are you param Write؟ {canWrite}");

        //// إزالة صلاحية Read
        //userPermissions &= ~Permissions.Read;
        //Console.WriteLine($"after delete Read: {userPermissions}");

        //// التحقق إذا كان لديه صلاحية Delete
        //bool canDelete = (userPermissions & Permissions.Delete) == Permissions.Delete;
        //Console.WriteLine($"are you param Delete؟ {canDelete}");

        //// إزالة جميع الصلاحيات
        //userPermissions = Permissions.None;
        //Console.WriteLine($"aafter fel all: {userPermissions}");
    }
}
