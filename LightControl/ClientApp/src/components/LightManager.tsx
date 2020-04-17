import * as React from "react";
import { LightClient } from "../models/LightClient";
import { LightDisplay } from "./LightDisplay";

interface ILightManagerProps {}
interface ILightManagerState {
  clients: LightClient[];
  current: LightClient | null;
  currentKey: string | null;
  loading: boolean;
}

export class LightManager extends React.Component<
  ILightManagerProps,
  ILightManagerState
> {
  static displayName = LightManager.name;

  constructor(props: ILightManagerProps) {
    super(props);

    this.state = {
      clients: [],
      current: null,
      currentKey: null,
      loading: true
    };
  }
  componentDidMount() {
    this.getLightClients();
  }

  renderLightModulesTable(clients: LightClient[]) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>type</th>
            <th>Number of Lights</th>
          </tr>
        </thead>
        <tbody>
          {clients.map(client => (
            <tr key={client.id} onClick={() => "uno"}>
              <td>{client.id}</td>
              <td>{client.name}</td>
              <td>{client.type}</td>
              <td>{client.lights.length}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderLightModulesTable(this.state.clients)
    );

    return (
      <div>
        <div>
          <h1 id="tableLabel">Light Modules</h1>
          {contents}
        </div>
        <div className="selected-light">
          <h2>Selected Light Module</h2>
          <LightDisplay client={this.state.current} />
        </div>
      </div>
    );
  }

  getLightClients() {
    // const response = await fetch("clients");
    // const data = await response.json();

    // let clients = data as LightClient[];

    const clients: LightClient[] = [
      {
        id: "uno",
        name: "test-strand",
        type: "strand",
        lights: [{ value: 255 }, { value: 24 }, { value: 106 }]
      },
      {
        id: "dos",
        name: "est-rand",
        type: "panel",
        lights: [{ value: 152 }, { value: 81 }, { value: 78 }, { value: 41 }]
      }
    ];

    this.setState({ clients: clients, loading: false });
  }
}
