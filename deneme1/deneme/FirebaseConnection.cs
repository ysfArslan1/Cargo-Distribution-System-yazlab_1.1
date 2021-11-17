using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
namespace deneme
{
    class FirebaseConnection//baglanti olusturma
    {

        public IFirebaseConfig config = new FirebaseConfig
        {

            AuthSecret = "yKl7zFGq4Lp6DvjftJY6ixQU3DYajCcRG8edVN5y",
            BasePath = "https://yazlab-34e31-default-rtdb.firebaseio.com/"
        };
        public IFirebaseClient client;
        public FirebaseResponse response;
    }
}
