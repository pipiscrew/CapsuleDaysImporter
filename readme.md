# CapsuleDaysImporter

A tiny app to import article that you hold on the go. User has to add the information to a text file and pass it to application. An example of input file is :  

```
~2026-01-28
7:50 toast + mayo #breakfast
7:50 150ml milk
7:57 2xMG #supplement
8:00 Freddo espresso
9:10 1xBetaine #supplement
9:20 Flakes with 200ml milk #breakfast
9:30 toast + mayo #premeal
12:40 1xFish soup #meal
13:00 Peanut butter with honey tost
13:30 nuts 65gr
14:50 Crackers 74gr
```

for multiple days :  

```
~2026-01-28
7:50 toast + mayo #breakfast
7:50 150ml milk
~2026-01-29
7:57 2xMG #supplement
8:00 Freddo espresso
9:10 1xBetaine #supplement
9:20 Flakes with 200ml milk #breakfast
9:30 toast + mayo #premeal
~2026-01-30
12:40 1xFish soup #meal
13:00 Peanut butter with honey tost
13:30 nuts 65gr
14:50 Crackers 74gr
```

## instructions of use
By default the [CapsuleDays](https://wolfeign.lsv.jp/capsule-days/en/index.html) application advise the user to create credentials, these allows the `dbase` to be encrypted.  

To use this repo tiny app, you have to clear the credentials, by doing the following :  

![Image](https://github.com/user-attachments/assets/da06d06e-e6d0-481e-9207-3de32b03eb0a)

afterwards copy the tiny app near the CapsuleDays dbase file, knowing at :
> app\capsule-days-users\New User

![Image](https://github.com/user-attachments/assets/f9b9ae8d-b4ad-4ae0-a24e-f351bb3a21a0)

then execute the tiny app from there!

Greets fly to Wolfeign for [CapsuleDays](https://wolfeign.lsv.jp/capsule-days/en/index.html)!!

# This project uses the following 3rd-party dependencies :  
* [SQLite.NET.dll](http://adodotnetsqlite.sourceforge.net/)
* [sqlite3.dll](https://sqlite.org/)

## This project is no longer maintained
Copyright (c) 2026 [PipisCrew](http://pipiscrew.com)

Licensed under the [MIT license](http://www.opensource.org/licenses/mit-license.php).  