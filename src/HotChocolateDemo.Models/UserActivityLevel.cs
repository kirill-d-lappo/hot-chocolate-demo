﻿namespace HotChocolateDemo.Models;

[Flags]
public enum UserActivityLevel : short
{
  Basic = 0,
  Advanced = 1,
  Pro = 2,
}