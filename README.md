#### ARnActorModel
A C# Actor Model library.

I was looking for an actor model things with some requirements :
 - no dependancy from another library
 - coding in plain C#, with such things like 'SendMessage', 'new Actor()', 'Become(behavior)'
 - actor can change behaviors
 - actor can send message
 - actor can create other actors
 - behaviors are dynamic
 - actor can send message across servers
 
#### With ARnActor, now you can :

    var sender = new BaseActor() ;
    var receiver = new BaseActor() ;
 
 and in sender code ..
 
    receiver.SendMessage("something") ;
 
#### The library holds some useful features :
-  behavior can be attached or removed from actor, (an actor can change it's own behavior ...)
-  actor can send messages across servers, you just need to hold a reference to another actor on a server ...
-  some actor can behave as public services, or be supervised

![Coverage](https://github.com/SyndARn/ARnActorModel/ARnActorSolution/TestActor/Report/badge_combined.svg)
Unit tests are included as well as some sample applications.

I used the excellent OpenCover to give some tests coverage.

For a common usage, you can find SyndARn here in : [![Nuget](https://buildstats.info/nuget/ARnActorModel)](http://nuget.org/packages/ARnActorModel) 

#### Current works

- more coverage
- moving to PCL/Universal/Shared model
 

