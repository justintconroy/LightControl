import * as React from "react";
import { Color, ColorResult, SketchPicker } from "react-color";

interface ILightProps {}
interface ILightState {
  color: Color;
  showPicker: boolean;
}

export class Light extends React.Component<ILightProps, ILightState> {
  constructor(props: ILightProps) {
    super(props);
    this.state = { color: "#000000", showPicker: false };
  }
  handleChange = (color: ColorResult) => {
    this.setState({ color: color.hex });
  };

  handleClick = () => {
    this.setState({ showPicker: !this.state.showPicker });
  };

  render() {
    return (
      <div>
        <svg>
          <circle
            cx={50}
            cy={50}
            r={20}
            fill={this.state.color as string}
            onClick={this.handleClick}
          />
        </svg>
        {this.state.showPicker && (
          <div>
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
