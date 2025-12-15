namespace AdvanceCRM.ExtraSamples {
    export const ReactSampleApp: React.FC = () => {
        return <div>Hello from React Sample!</div>;
    };

    export function init() {
        ReactDOM.render(<ReactSampleApp />, document.getElementById('ReactSampleRoot'));
    }
}
