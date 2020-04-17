import * as React from "react";

interface ILightProps {
  value: number;
  onClick: () => void;
}

export function Light(props: ILightProps) {
  return (
    <button className="light" onClick={props.onClick}>
      {props.value}
    </button>
  );
}
