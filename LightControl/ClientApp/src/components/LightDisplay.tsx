import * as React from "react";
import { LightClient } from "../models/LightClient";

interface ILightDisplayProps {
  client: LightClient | null;
}
interface ILightDisplayState {}

export class LightDisplay extends React.Component<
  ILightDisplayProps,
  ILightDisplayState
> {
  static displayName = LightDisplay.name;

  constructor(props: ILightDisplayProps) {
    super(props);
  }

  renderLightClient(client: LightClient) {
    return <div></div>;
  }

  render() {
    let content =
      this.props.client === null ? (
        <div></div>
      ) : (
        this.renderLightClient(this.props.client)
      );
    return <div></div>;
  }
}
