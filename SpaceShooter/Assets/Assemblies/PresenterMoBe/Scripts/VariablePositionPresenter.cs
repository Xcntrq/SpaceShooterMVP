namespace PresenterMoBe
{
    using System;
    using SpaceShooterGame.Contracts.Public;
    using UnityEngine;

    [RequireComponent(typeof(Transform))]
    internal class VariablePositionPresenter : EntityPresenter, IVariablePosition
    {
        private GamePresenter _gamePresenter;
        private IVariablePosition _variablePosition;

        public event Action PositionChanged;

        public (float, float) Position => _variablePosition.Position;

        internal override void Initialize(GamePresenter gamePresenter, IPresentable presentableEntity)
        {
            _gamePresenter = gamePresenter;
            _variablePosition = presentableEntity as IVariablePosition;

            VariablePosition_PositionChanged();
            _variablePosition.PositionChanged += VariablePosition_PositionChanged;
        }

        private void VariablePosition_PositionChanged()
        {
            Vector2 newPos = new(Position.Item1, Position.Item2);
            transform.localPosition = _gamePresenter.ViewportToWorldPoint(newPos);
            PositionChanged?.Invoke();
        }

        private void OnDestroy()
        {
            if (_variablePosition != null)
            {
                _variablePosition.PositionChanged -= VariablePosition_PositionChanged;
            }
        }
    }
}