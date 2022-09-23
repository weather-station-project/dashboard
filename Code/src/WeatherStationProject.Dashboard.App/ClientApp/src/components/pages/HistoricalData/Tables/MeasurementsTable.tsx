import React from 'react';
import { useTranslation } from 'react-i18next';
import BootstrapTable, { ColumnDescription } from 'react-bootstrap-table-next';
import paginationFactory from 'react-bootstrap-table2-paginator';
import ToolkitProvider, { ToolkitContextType } from 'react-bootstrap-table2-toolkit';
import { paginationOptions } from './BaseConfig';

interface IMeasurementsTablesProps {
  measurements: object[];
  columns: object[];
  columnNameSort: string;
  csvFilename: string;
}

// Doc -> https://react-bootstrap-table.github.io/react-bootstrap-table2/

const MeasurementsTable: React.FC<IMeasurementsTablesProps> = ({
  measurements,
  columns,
  columnNameSort,
  csvFilename,
}) => {
  const { t } = useTranslation();
  const ExportCSVButton = (props: { onExport: () => void }) => {
    const handleClick = () => {
      props.onExport();
    };
    return (
      <div>
        <button className="btn btn-success" onClick={handleClick}>
          {t('historical_data.list.export_csv')}
        </button>
      </div>
    );
  };

  return (
    <ToolkitProvider
      bootstrap4
      keyField="id"
      data={measurements}
      columns={columns as ColumnDescription[]}
      exportCSV={{
        fileName: `${csvFilename}.csv`,
        separator: ';',
        blobType: 'text/csv;charset=utf8',
      }}
    >
      {(props: ToolkitContextType) => (
        <div>
          <ExportCSVButton {...props.csvProps} />
          <hr />
          <BootstrapTable
            {...props.baseProps}
            pagination={paginationFactory(paginationOptions)}
            defaultSorted={[
              {
                dataField: columnNameSort,
                order: 'desc',
              },
            ]}
          />
        </div>
      )}
    </ToolkitProvider>
  );
};

export default MeasurementsTable;
