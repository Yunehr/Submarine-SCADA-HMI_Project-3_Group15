import React from 'react';
import GaugeComponent from 'react-gauge-component';
import './App.css';

const CustomGauge = ({ grad = false, value = 40, min = 0, max = 100, dLow = 20, wLow = 30, wHigh = 70, dHigh = 80, labelType = "", showLow = true, showHigh = true, showMinMax = false }) => {
    const v = value;

    return (
        <div className="gauge-wrapper">
            <GaugeComponent
                type="semicircle"
                arc={{
                    width: 0.2,
                    padding: 0.005,
                    cornerRadius: 0,
                    gradient: grad,
                    subArcs: [
                        {
                            limit: dLow,
                            color: '#EA4228',
                            showTick: showLow
                        },
                        {
                            limit: wLow,
                            color: '#F5CD19',
                            showTick: true
                        },
                        {
                            limit: wHigh,
                            color: '#5BE12C',
                            showTick: showHigh
                        },
                        {
                            limit: dHigh,
                            color: '#F5CD19',
                            showTick: showHigh
                        },
                        {
                            limit: max,
                            color: '#EA4228',
                            showTick: false
                        },
                    ]
                }}
                pointer={{
                    type: 'arrow',
                    color: '#345243',
                    length: 0.80,
                    width: 15,
                    elastic: true,
                }}
                labels={{
                    valueLabel: {
                        formatTextValue: value => value + labelType,
                        style: {
                            fontSize: 20,
                            fill: "#345243",
                            textShadow: false
                        }
                    },
                    tickLabels: {
                        type: 'outer',
                        hideMinMax: !showMinMax,
                        defaultTickValueConfig: {
                            formatTextValue: value => value + labelType,
                            style: { fontSize: 8 },
                        }
                    }
                }}
                value={v}
                minValue={min}
                maxValue={max}
            />
        </div>
    );
};

export default CustomGauge;