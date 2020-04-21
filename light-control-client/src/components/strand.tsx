import * as React from "react";
import { Color, ColorResult } from "react-color";
import { Light } from "./light";

interface IStrandProps {}
interface IStrandState {
  lights: Color[];
  selected: number | null;
  isLoaded: boolean;
}

export class Strand extends React.Component<IStrandProps, IStrandState> {
  constructor(props: IStrandProps) {
    super(props);
    this.state = {
      lights: Array(0),
      selected: null,
      isLoaded: false,
    };
  }

  componentDidMount() {
    fetch("https://localhost:5001/api/strands/1")
      .then((response) => response.json())
      .then(
        (data) => {
          const lights: string[] = data.lights.map(
            (light: any) => light.color as string
          );
          this.setState({ lights: lights, isLoaded: true });
        },
        (_error) =>
          this.setState({
            lights: Array(0),
          })
      );
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

  handleChangeComplete = (i: number, color: ColorResult) => {
    const opts = {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ color: color.hex }),
    };

    fetch(`https://localhost:5001/api/lights/${i + 1}`, opts);
  };

  render() {
    return this.state.isLoaded ? (
      <div className="strand">
        {this.state.lights.map((light, i) => {
          return (
            <Light
              color={light}
              isSelected={i === this.state.selected}
              onClick={() => this.handleClick(i)}
              onChange={(color: ColorResult) => this.handleChange(i, color)}
              onChangeComplete={(color: ColorResult) =>
                this.handleChangeComplete(i, color)
              }
            />
          );
        })}
      </div>
    ) : (
      <div className="strand">Strand not loaded!</div>
    );
  }
}
