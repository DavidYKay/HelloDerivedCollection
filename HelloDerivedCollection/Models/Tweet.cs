﻿using System;

namespace HelloDerivedCollection
{
  public class Tweet 
  {
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }

    override public string ToString() {
      return Title;
    }
  }
}

