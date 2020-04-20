import * as React from "react";
import { Color, ColorResult, SketchPicker } from "react-color";

interface ILightProps {
  color: Color;
  isSelected: boolean;
  onClick: () => void;
  onChange: (color: ColorResult) => void;
}

export class Light extends React.Component<ILightProps> {
  render() {
    return (
      <div className="light">
        <svg>
          <circle
            cx={25}
            cy={25}
            r={20}
            stroke="#252525"
            stroke-width={this.props.isSelected ? "4" : 0}
            fill={this.props.color as string}
            onClick={this.props.onClick}
          />
        </svg>
        {this.props.isSelected && (
          <span className="light-picker">
            <SketchPicker
              color={this.props.color}
              disableAlpha={true}
              onChange={this.props.onChange}
            />
          </span>
        )}
      </div>
    );
  }
}
