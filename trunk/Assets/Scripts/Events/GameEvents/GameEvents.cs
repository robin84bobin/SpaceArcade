using UnityEngine;

public class HeroDeathEvent : xEvent{}
public class HeroSpawnEvent : xEvent{ }
public class GameOverEvent : xEvent{ }
public class AddScoreEvent : xParamEvent<int> {}
public class UpdateScoreEvent : xParamEvent<int> { }
public class UpdateLivesEvent : xParamEvent<int> { }

