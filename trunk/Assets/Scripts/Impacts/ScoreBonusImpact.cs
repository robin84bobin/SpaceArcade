using System;

public class ScoreBonusImpact : CollisionImpact
{
    public int scoreValue = 10;

    protected override void OnApply(EntityModel entity)
    {
        EventManager.Get<AddScoreEvent>().Publish(scoreValue);
        OnAppliedComplete();
    }
}
