import * as React from "react";
import { Color, ColorResult } from "react-color";
import { Light } from "./light";

interface IStrandProps {}
interface IStrandState {
  lights: Color[];
  selected: number | null;
}

export class Strand extends React.Component<IStrandProps, IStrandState> {
  constructor(props: IStrandProps) {
    super(props);
    this.state = {
      lights: Array(50).fill({ Color: "#000000", isSelected: false }),
      selected: null,
    };
  }

  handleClick = (i: number) => {
    const current = this.state.selected;

    // Clear the selection if the currently
    //  selected light is selected again.
    if (current === i) {
      this.setState({ selected: null });
      return;
    }

    this.setState({ selected: i });
  };

  handleChange = (i: number, color: ColorResult) => {
    const lights = this.state.lights.slice();
    lights[i] = color.hex;
    this.setState({ lights: lights });
  };

  render() {
    return (
      <div className="strand">
        {this.state.lights.map((light, i) => {
          return (
            <Light
              color={light}
              isSelected={i === this.state.selected}
              onClick={() => this.handleClick(i)}
              onChange={(color: ColorResult) => this.handleChange(i, color)}
            />
          );
        })}
      </div>
    );
  }
}
