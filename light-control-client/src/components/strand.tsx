import * as React from "react";
import { ILightProps, Light } from "./light";

interface IStrandProps {}
interface IStrandState {
  lights: ILightProps[];
}

export class Strand extends React.Component<IStrandProps, IStrandState> {
  constructor(props: IStrandProps) {
    super(props);
    this.state = {
      lights: Array(50).fill({ Color: "#000000", isSelected: false }),
    };
  }

  render() {
    return (
      <div>
        {this.state.lights.map((light) => {
          return <Light color={light.color} isSelected={light.isSelected} />;
        })}
      </div>
    );
  }
}
