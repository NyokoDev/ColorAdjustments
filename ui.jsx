import React from 'react'; // Import React

// MyPanel component
const ColorADPanel = () => {
    return (
        <Panel title="ColorAdjustments">
            <div className="field_MBO">
                <div className="label_DGc label_ZLb">Active vehicles</div>
                <h1>Help</h1>
            </div>
            <div></div>
        </Panel>
    );
}

// Registering panel
window._$hookui.registerPanel({
    id: 'nyoko.coloradjustments',
    name: 'Color Adjustments',
    icon: 'Media/PhotoMode/Camera.svg',
    component: ColorADPanel,
});
