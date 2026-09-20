using Microsoft.AspNetCore.Components;

namespace ReptiRealm_WebApp.Shared.Components
{
    public abstract class DialogBase : ComponentBase
    {
        [Parameter]
        public bool IsOpen { get; set; }

        [Parameter]
        public EventCallback<bool> IsOpenChanged { get; set; }

        // Override these for async open/close hooks
        protected virtual Task OnOpenAsync() => Task.CompletedTask;
        protected virtual Task OnCloseAsync() => Task.CompletedTask;

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            var isOpen = parameters.GetValueOrDefault<bool>(nameof(IsOpen));
            var isOpenChanged = isOpen != IsOpen;

            await base.SetParametersAsync(parameters);

            if (isOpenChanged)
            {
                if (isOpen)
                {
                    await OnOpenAsync();
                }
                else
                {
                    await OnCloseAsync();
                }
            }
        }
    }

    public abstract class DialogBase<T> : ComponentBase
    {
        [Parameter]
        public DialogState<T> State
        {
            get => new(Model, IsOpen);
            set
            {
                // When parent opens the dialog and supplies a model, capture it so
                // OnParametersSetAsync can react to the new model.
                if (value.IsOpen)
                {
                    _modelSet = true;
                    Model = value.Model;
                }

                _isOpen = value.IsOpen;
            }
        }

        [Parameter]
        public EventCallback<DialogState<T>> StateChanged { get; set; }

        protected T Model { get; private set; }

        protected bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;
                StateChanged.InvokeAsync(State);
            }
        }

        private bool _isOpen;
        private bool _modelSet;

        protected override async Task OnParametersSetAsync()
        {
            if (!_modelSet) return;

            OnModelSet();
            await OnModelSetAsync();
            _modelSet = false;
        }

        protected virtual Task OnModelSetAsync() => Task.CompletedTask;

        protected virtual void OnModelSet() { }
    }

    public readonly struct DialogState<T>
    {
        public bool IsOpen { get; }
        public T Model { get; }

        public DialogState(T model, bool isOpen = true)
        {
            Model = model;
            IsOpen = isOpen;
        }

        public static implicit operator DialogState<T>(T model) => new(model);
    }

    public static class DialogState
    {
        public static DialogState<T> Open<T>(T model) => new(model);

        public static DialogState<T> Closed<T>(T model) => new();
    }
}