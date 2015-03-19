using System;
using ReactiveUI;
using Splat;
using System.Runtime.Serialization;
//using ReactiveUI.XamForms;
//using Akavache;

namespace HelloDerivedCollection.ViewModels
{
  [DataContract]
  public class TweetViewModel : ReactiveObject, IRoutableViewModel
  {

    [IgnoreDataMember]
    public string UrlPathSegment { get { return "Hello"; } }

    // Not serializing the HostScreen is very important, because you
    // will create a loop in the object graph => crash
    [IgnoreDataMember]
    public IScreen HostScreen { get; protected set; }
    
    [DataMember]
    public ReactiveList<Tweet> Tweets { get; protected set; }
    [IgnoreDataMember]
    public IReactiveDerivedList<TweetTileViewModel> TweetTiles;
    [IgnoreDataMember]
    public IReactiveDerivedList<TweetTileViewModel> VisibleTiles;

    public TweetViewModel (IScreen screen = null) {
      HostScreen = screen ?? Locator.Current.GetService<IScreen>();
      
      Tweets     = new ReactiveList<Tweet>();
      Tweet[] tweets = {
        new Tweet{
          Title = "Hello Twitter",
          CreatedAt = new DateTime(),
        },
        new Tweet{
          Title = "#impooping",
          CreatedAt = new DateTime(),
        },
        new Tweet{
          Title = "Internets",
          CreatedAt = new DateTime(),
        },
      };
      Tweets.AddRange (tweets);

      TweetTiles = Tweets.CreateDerivedCollection(
          x => new TweetTileViewModel(x),
          x => true,
          (x, y) => x.Model.CreatedAt.CompareTo(y.Model.CreatedAt));

      VisibleTiles = TweetTiles.CreateDerivedCollection(
          x => x,
          x => x.IsVisible);
    }
  }

  [DataContract]
  public class TweetTileViewModel : ReactiveObject
  {
    [DataMember]
    public Tweet Model { get; protected set; }

    [IgnoreDataMember] bool isVisible = true;
    [DataMember] public bool IsVisible {
      get { return isVisible; }
      set { this.RaiseAndSetIfChanged(ref isVisible, value); }
    }

    //public ReactiveCommand<Object> SelectThisHello { get; protected set; }

    public TweetTileViewModel(Tweet model, IScreen hostScreen = null)
    {
      hostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

      Model = model;

     // SelectThisHello = ReactiveCommand.CreateAsyncObservable(_ =>
     //     hostScreen.Router.Navigate.ExecuteAsync(new PreliminaryMenuViewModel(model, hostScreen)));
    }
  }
}

