import * as React from "react";
import { Color, ColorResult, SketchPicker } from "react-color";

interface ILightProps {}
interface ILightState {
  color: Color;
  isSelected: boolean;
}

export class Light extends React.Component<ILightProps, ILightState> {
  constructor(props: ILightProps) {
    super(props);
    this.state = { color: "#000000", isSelected: false };
  }
  handleChange = (color: ColorResult) => {
    this.setState({ color: color.hex });
  };

  handleClick = () => {
    this.setState({ isSelected: !this.state.isSelected });
  };

  render() {
    return (
      <div>
        <svg className="SingleLight">
          <circle
            cx={50}
            cy={50}
            r={20}
            stroke="black"
            stroke-width={this.state.isSelected ? "4" : 0}
            fill={this.state.color as string}
            onClick={this.handleClick}
          />
        </svg>
        {this.state.isSelected && (
          <div className="SingleLight-Picker">
            <SketchPicker
              color={this.state.color}
              disableAlpha={true}
              onChange={this.handleChange}
            />
          </div>
        )}
      </div>
    );
  }
}
