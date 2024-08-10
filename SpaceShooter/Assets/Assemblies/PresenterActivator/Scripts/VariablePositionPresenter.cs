namespace PresenterActivator
{
    using System;
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    internal class VariablePositionPresenter : EntityPresenter, IVariablePosition
    {
        private readonly IVariablePosition _variablePosition;

        public VariablePositionPresenter(GamePresenter gamePresenter, IPresentable presentableEntity, ViewableEntity viewableEntity) : base(gamePresenter, presentableEntity, viewableEntity)
        {
            _variablePosition = PresentableEntity as IVariablePosition;

            VariablePosition_PositionChanged();
            _variablePosition.PositionChanged += VariablePosition_PositionChanged;
        }

        public event Action PositionChanged;

        public (float, float) Position => _variablePosition.Position;

        private void VariablePosition_PositionChanged()
        {
            Vector2 newPos = new(Position.Item1, Position.Item2);
            ViewableEntity.Position = GamePresenter.ViewportToWorldPoint(newPos);
            PositionChanged?.Invoke();
        }
    }
}