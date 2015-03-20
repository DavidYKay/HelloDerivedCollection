using System;
using ReactiveUI;
using Splat;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace HelloDerivedCollection.ViewModels
{
  [DataContract]
  public class TweetViewModel : ReactiveObject, IRoutableViewModel
  {

    [IgnoreDataMember]
    public string UrlPathSegment { get { return "Search"; } }

    [IgnoreDataMember]
    public IScreen HostScreen { get; protected set; }
    
    [DataMember]
    public ReactiveList<TweetTileViewModel> TweetTiles { get; protected set; }

    [IgnoreDataMember]
    public IReactiveDerivedList<TweetTileViewModel> VisibleTiles;
    
    [IgnoreDataMember]
    public ReactiveCommand<Object> HideTweet;
    
    [IgnoreDataMember]
    public ReactiveCommand<Object> Search;
    
    [IgnoreDataMember] string searchQuery;
    [DataMember] public string SearchQuery {
      get { return searchQuery; }
      set { this.RaiseAndSetIfChanged(ref searchQuery, value); }
    }

    public TweetViewModel (IScreen screen = null) {
      HostScreen = screen ?? Locator.Current.GetService<IScreen>();
      
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
        new Tweet{
          Title = "Food",
          CreatedAt = new DateTime(),
        },
        new Tweet{
          Title = "Dating",
          CreatedAt = new DateTime(),
        },
        new Tweet{
          Title = "Alternative Medicine",
          CreatedAt = new DateTime(),
        },
      };
      
      HideTweet = ReactiveCommand.Create ();
      HideTweet.Subscribe( _ => {
          Log.Debug("hiding a tweet");
          using (TweetTiles.SuppressChangeNotifications()) {
            var tweet = VisibleTiles[0];
            var index = TweetTiles.IndexOf(tweet);
            TweetTiles[index].IsVisible = false;
          }
          });
      
      TweetTiles     = new ReactiveList<TweetTileViewModel>();
      foreach (var tweet in tweets) {
        TweetTiles.Add(new TweetTileViewModel(this, tweet));
      }

      VisibleTiles = TweetTiles.CreateDerivedCollection(
          x => x,
          x => x.IsVisible);
      
      Search = ReactiveCommand.Create ();
      Search.Subscribe( _ => {
          using (TweetTiles.SuppressChangeNotifications()) {
            Log.Debug("performing search with query: " + SearchQuery);
            foreach (var tile in TweetTiles) {
              tile.IsVisible = SearchMatch(tile.Model.Title, SearchQuery);
            }
          }
        });

       this.WhenAnyValue(x => x.SearchQuery)
        .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
        .InvokeCommand(this, x => x.Search);
    }

    private bool SearchMatch(string target, string regex) {
      if (String.IsNullOrEmpty(regex)) { return true; } 
      if (String.IsNullOrEmpty(target)) { return false; } 
      return target.ToLower().Contains(regex.ToLower());
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

    public TweetTileViewModel(TweetViewModel parent, Tweet model, IScreen hostScreen = null)
    {
      hostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

      Model = model;
    }
  }
}

